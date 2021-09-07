using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Core.DataAccess.Dapper;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Data.SqlClient;

namespace DataAccess.Concrete.Dapper
{
    public class DapperOrderDal : DapperRepositoryBase<SqlConnection, Order>, IOrderDal
    {
        private string _connString;

        public DapperOrderDal()
        {
            _connString = ConnectionFactory.GetConnectionString();
        }
        public List<OrderDetailDto> GetOrderDetails(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                var result = conn.Query<Order, Customer, OrderDetailDto>(sql, (order, customer) =>
                    {
                        return new OrderDetailDto()
                        {
                            OrderDate = (DateTime)order.OrderDate,
                            CompanyName = customer.CompanyName,
                            ContactName = customer.ContactName
                        };
                    },
                    splitOn: "CustomerID"
                );
                return result.ToList();
            }
        }
    }
}
