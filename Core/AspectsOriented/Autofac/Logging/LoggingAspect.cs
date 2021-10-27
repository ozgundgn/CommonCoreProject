using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;

namespace Core.AspectsOriented.Autofac.Logging
{
    public class LoggingAspect:Modellnterception
    {
        private LoggingBaseService _loggingBaseService;
        public LoggingAspect(Type loggerClass)
        {
            if (loggerClass.BaseType != typeof(LoggingBaseService))
            {
                throw new System.Exception("AspectMessages.WrongLoggerType");
            }

            _loggingBaseService = (LoggingBaseService)Activator.CreateInstance(loggerClass);
        }
        protected override void OnAfter(IInvocation invocation)
        {

            _loggingBaseService.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            List<LogParameter> logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type=invocation.Arguments[i].GetType().Name

                });
            }

        }
    }

}
