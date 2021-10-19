using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Remotion.Linq.Clauses.ResultOperators;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<ModelIntercepitonBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<ModelIntercepitonBaseAttribute>();
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x=>x.Priority).ToArray();
        }
    }
}
