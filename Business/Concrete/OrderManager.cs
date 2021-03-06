using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.AspectsOriented.Autofac.Caching;
using Core.AspectsOriented.Autofac.Exception;
using Core.AspectsOriented.Autofac.Logging;
using Core.AspectsOriented.Autofac.Performance;
using Core.AspectsOriented.Autofac.Validation;
using Core.Utilities.BusinessRules;
using DataAccess.Abstract;
using Entities.DTOs;
using FluentValidation;
using Core.AspectsOriented.Autofac.Transaction;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        [SecuredOperation("order.add,admin")]
        [ValidationAspect(typeof(OrderValidator))]
        [CachingRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {

            //var context = new ValidationContext<Order>(order);
            //OrderValidator orderValidator = new OrderValidator();
            //var result = orderValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException("");
            //}
            //İş mantığı yazılma şekli Core da IResult dönen bir method
            //IResult result = BusinessRules.Run(CheckIfShipNameExists(order.ShipName),CheckIfCountFreightIsCorrect(order.Freight));
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

        [LoggingAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Order>> GetAllByShippedName(string shippedName)
        {
            var list = _orderDal.GetAll(x => x.ShipName.Contains(shippedName));
            return new SuccessDataResult<List<Order>>(list);
        }
        [CachingAspect]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Order>> GetAll()
        {
            var list = _orderDal.GetAll();
            return new SuccessDataResult<List<Order>>(list);
        }
        [CachingAspect]
        [PerformanceAspect(30)]
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
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<OrderDetailDto>> GetOrderDetails(string sql)
        {
            var orderDetails = _orderDal.GetOrderDetails(sql);
            var n = 0;
            // ReSharper disable once IntDivisionByZero
            var b = 1 / n;


            return new SuccessDataResult<List<OrderDetailDto>>(orderDetails);
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Order order)
        {
            
            Add(order);
            if (order.Freight < 10)
            {
                throw new Exception("Freight alanı 10'dan küçük olamaz.");
            }
            return null;
        }
    }
}
