using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.DataAccess;
using Core.DataAccess.Nhibernate;
using Core.Extensions.ORMExtensions.NhibernateExtension;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;

namespace DataAccess.Concrete.Nhibernate
{
    public class NhibernateOrderDal : NhibernateRepositoryBase<SqlClientDriver, MsSql2008Dialect, Order>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetails(string sql)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var newObject = session.CreateSQLQuery(sql).AddScalars<Order>().Enumerable<Order>();
                    tx.Commit();
                    return null;
                }
            }
        }
    }
}
