using System;
using System.Collections.Generic;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
   public class CoreModule:ICoreModule // autofac değil microsoft dependency inj.
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();// arkadaki IMemoryCache in karşılığını buradan alıyor memeorychche instance ı oluşuyor
            serviceCollection.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager,CacheManager>();
        }
    }
}
