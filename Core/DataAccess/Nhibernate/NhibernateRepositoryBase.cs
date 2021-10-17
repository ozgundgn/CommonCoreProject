using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Entities;
using Core.Extensions.ORMExtensions.NhibernateExtension;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Transform;
using Remotion.Linq.Clauses.ResultOperators;
namespace Core.DataAccess.Nhibernate
{

  
    public class NhibernateRepositoryBase<TDriver, TDialect, TEntity> : IEntityRepository<TEntity>
          where TDriver : class, IDriver, new() where TDialect : Dialect where TEntity : class, IEntity, new()
    {
        private Configuration _cfg;
        protected ISessionFactory _sessionFactory;
        private string _connString;
        public NhibernateRepositoryBase()
        {
            _sessionFactory = InitSessionFactory();
            _connString = ConnectionFactory.GetConnectionString();
        }

        public ISessionFactory InitSessionFactory()
        {
            //_cfg = (new Configuration()).Configure(configurationFilePath);

            _cfg = new Configuration();

            _cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = @"Data Source=OZGUN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog= Northwind";
                x.Driver<TDriver>();
                x.Dialect<TDialect>();
                x.LogSqlInConsole = true;
            });
            _cfg.AddAssembly(Assembly.GetExecutingAssembly());
            _cfg.SessionFactory().GenerateStatistics();
            return _cfg.BuildSessionFactory();
        }

        public long? Add(TEntity entity)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var newObject = session.Save(entity);
                    tx.Commit();
                    return null;
                }
            }
        }

        public bool? Delete(TEntity entity)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Delete(entity);
                    tx.Commit();
                    return null;
                }
            }
        }

        public bool? Update(TEntity entity)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Update(entity);
                    tx.Commit();
                    return null;
                }
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
