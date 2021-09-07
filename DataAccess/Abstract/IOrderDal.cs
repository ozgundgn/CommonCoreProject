#nullable enable
using Core.DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
       public List<OrderDetailDto> GetOrderDetails(string? sql);
    }
}
