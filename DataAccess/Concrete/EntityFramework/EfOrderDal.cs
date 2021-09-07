#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataAccess.Concrete
{
    public class EfOrderDal : EntityFrameworkRepositoryBase<Order, NorthwindContext>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetails(string? sql)
        {
            using NorthwindContext context = new NorthwindContext();
            var result = from o in context.Orders
                         join c in context.Customers
                             on o.CustomerID equals c.CustomerID
                         select new OrderDetailDto()
                         {
                             OrderDate = (DateTime)o.OrderDate,
                             CompanyName = c.CompanyName,
                             ContactName = c.ContactName
                         };
            return result.ToList();
        }
    }
}
