using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Dapper.Contrib.Extensions;

namespace Entities.Concrete
{
    [Table("Customers")]
    public class Customer : IEntity
    {
        [Key]
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
    }
}
