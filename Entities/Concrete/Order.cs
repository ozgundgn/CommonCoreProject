using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Entities;
using Dapper.Contrib.Extensions;

namespace Entities.Concrete
{
    [Dapper.Contrib.Extensions.Table("Orders")]
    public class Order : IEntity
    {
        [Key]
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
    }
}
