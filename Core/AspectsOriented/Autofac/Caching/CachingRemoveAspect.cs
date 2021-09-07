using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.AspectsOriented.Autofac.Caching
{
    public class CachingRemoveAspect : Modellnterception
    {
        private ICacheManager _cacheManager;
        private string _pattern;

        public CachingRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
           _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
