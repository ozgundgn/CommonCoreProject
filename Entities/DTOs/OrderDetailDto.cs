using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
    public class OrderDetailDto : IDto
    {
        public DateTime OrderDate { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
}
