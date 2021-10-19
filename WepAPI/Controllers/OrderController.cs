using Business.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using NHibernate.Loader;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService { get; set; }

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public Order Get()
        {
            return new Order()
            {
                OrderID = 21413
            }; //_orderService.Get(new Order() { OrderID = id }).Data;
        }
    }
}
