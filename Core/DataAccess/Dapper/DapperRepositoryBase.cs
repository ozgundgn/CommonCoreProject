using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        public long Add(TEntity entity)
        {
            try
            {
                using (TDbConnection conn = new TDbConnection())
                {
                    conn.ConnectionString = _connString;
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    return conn.Insert<TEntity>(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        public bool Delete(TEntity entity)
        {
            try
            {
                using (TDbConnection conn = new TDbConnection())
                {
                    conn.ConnectionString = _connString;
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    return conn.Delete<TEntity>(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public bool Update(TEntity entity)
        {
            try
            {
                using (TDbConnection conn = new TDbConnection())
                {
                    conn.ConnectionString = _connString;
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    return conn.Update<TEntity>(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                using (TDbConnection conn = new TDbConnection())
                {
                    conn.ConnectionString = _connString;
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    return conn.GetAll<TEntity>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return default(List<TEntity>);
        }

        public TEntity Get(TEntity entity)
        {
            try
            {
                using (TDbConnection conn = new TDbConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.ConnectionString = _connString;

                    conn.Open();

                    return conn.Get<TEntity>(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return default(TEntity);
        }
    }
}
