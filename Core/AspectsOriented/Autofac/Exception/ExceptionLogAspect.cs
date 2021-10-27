using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.AspectsOriented.Autofac.Exception
{
    public class ExceptionLogAspect : Modellnterception
    {
        protected override void OnException(IInvocation invocation)
        {

            Console.WriteLine("Exception aspect run.");

        }
    }
}
