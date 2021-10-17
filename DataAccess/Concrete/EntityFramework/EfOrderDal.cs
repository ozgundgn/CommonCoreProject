#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EntityFrameworkRepositoryBase<Order, NorthwindContext>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetails(string? sql)
        {
            using NorthwindContext context = new NorthwindContext();
            var result = context.Orders.Join(context.Customers, o => o.CustomerID, c => c.CustomerID,
                (o, c) => new OrderDetailDto()
                {
                    OrderDate = (DateTime) o.OrderDate, CompanyName = c.CompanyName, ContactName = c.ContactName
                });
            return result.ToList();
        }
    }
}
