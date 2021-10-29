using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;

namespace Core.AspectsOriented.Autofac.Exception
{
    public class ExceptionLogAspect : Modellnterception
    {
        private LoggingBaseService _loggingBaseService;
        public ExceptionLogAspect(Type serviceType)
        {
            if (serviceType.BaseType != typeof(LoggingBaseService))
            {
                throw new System.Exception("AspectMessages.WrongLoggerType");
            }

            _loggingBaseService = (LoggingBaseService)Activator.CreateInstance(serviceType);
        }

        protected override void OnException(IInvocation invocation,System.Exception e)
        {
            LogDetailWithException logDetailWithException=GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _loggingBaseService.Error(logDetailWithException);
        }


        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            List<LogParameter> logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                if (invocation.Arguments[i] != null)
                {
                    logParameters.Add(new LogParameter()
                    {
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name

                    });
                }
            }

            var logDetail = new LogDetailWithException()
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
