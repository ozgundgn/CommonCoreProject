using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();
            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
        }
    }
}
