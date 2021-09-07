using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public IDataResult<long> Add(Order order)
        {
            var insertedID = _orderDal.Add(order);
            if (insertedID > 0)
                return new SuccessDataResult<long>(insertedID, "Sipariş kaydedildi.");

            return new ErrorDataResult<long>("Sipariş kaydedilmedi.");
        }

        public IResult Delete(int id)
        {

            if (_orderDal.Delete(new Order() { OrderID = id }))
                return new SuccessResult();

            return new ErrorResult();
        }


        public IDataResult<List<Order>> GetAllByShippedName(string shippedName)
        {
            var list = _orderDal.GetAll(x=>x.ShipName.Contains(shippedName));
            return new SuccessDataResult<List<Order>>(list);
        }

        public IDataResult<List<Order>> GetAll()
        {
            var list = _orderDal.GetAll();
            return new SuccessDataResult<List<Order>>(list);
        }

        public IDataResult<Order> GetById(int orderId)
        {
            var order = _orderDal.Get(new Order() { OrderID = orderId });
            return new SuccessDataResult<Order>(order);
        }
        public IDataResult<Order> Get(Order order)
        {
            var result = _orderDal.Get(order);
            return new SuccessDataResult<Order>(result);
        }

        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult();
        }
        public IDataResult<List<OrderDetailDto>> GetOrderDetails(string sql)
        {
            var orderDetails = _orderDal.GetOrderDetails(sql);

            return new SuccessDataResult<List<OrderDetailDto>>(orderDetails);
        }
    }
}
