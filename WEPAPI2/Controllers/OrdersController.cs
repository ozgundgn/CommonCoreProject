using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WEPAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        IOrderService _orderService { get; set; }

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("get/{id}")]
        public Order Get(int id)
        {
            return _orderService.GetById(id).Data;
        }
    }
}
