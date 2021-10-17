using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.AspectsOriented.Autofac.Validation;
using DataAccess.Abstract;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Add(Order order)
        {

            var context = new ValidationContext<Order>(order);
            OrderValidator orderValidator = new OrderValidator();
            var result = orderValidator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException("");
            }
            var insertedID = _orderDal.Add(order);
            if (insertedID > 0)
                return new SuccessResult( "Sipariş kaydedildi.");

            return new ErrorResult("Sipariş kaydedilmedi.");
        }

        public IResult Delete(int id)
        {
            var res = _orderDal.Delete(new Order() { OrderID = id });
            if (res != null && (bool)res)
                return new SuccessResult();

            return new ErrorResult();
        }


        public IDataResult<List<Order>> GetAllByShippedName(string shippedName)
        {
            var list = _orderDal.GetAll(x => x.ShipName.Contains(shippedName));
            return new SuccessDataResult<List<Order>>(list);
        }

        public IDataResult<List<Order>> GetAll()
        {
            var list = _orderDal.GetAll();
            return new SuccessDataResult<List<Order>>(list);
        }

        public IDataResult<Order> GetById(int orderId)
        {
            var order = _orderDal.Get(t=>t.OrderID==orderId);
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
