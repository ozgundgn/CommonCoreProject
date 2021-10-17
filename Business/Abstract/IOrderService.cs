using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int orderId);
        IResult Update(Order order);
        IResult Add(Order order);
        IDataResult<Order> Get(Order order);
        IResult Delete(int id);
        IDataResult<List<OrderDetailDto>> GetOrderDetails(string sql);
    }
}
