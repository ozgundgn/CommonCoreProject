using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Core.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Core.DataAccess.Dapper
{
    public class DapperRepositoryBase<TDbConnection, TEntity> : IEntityRepository<TEntity> where TDbConnection : class, IDbConnection, new()
    where TEntity : class, IEntity, new()
    {
        private string _connString { get; set; }

        public DapperRepositoryBase()
        {
            _connString = ConnectionFactory.GetConnectionString();

        }
        public void Add(TEntity entity)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                conn.Insert<TEntity>(entity);
            }
        }


        public void Delete(TEntity entity)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                conn.Delete<TEntity>(entity);
            }
        }

        public void Update(TEntity entity)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                conn.Update<TEntity>(entity);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> entity = null)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                return conn.GetAll<TEntity>().ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> entity = null)
        {
            using (TDbConnection conn = new TDbConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                return conn.Get<TEntity>(entity);
            }
        }

    }
}
