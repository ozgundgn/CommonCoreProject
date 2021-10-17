using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
   public class OrderValidator:AbstractValidator<Order>
   {
       public OrderValidator()
       {
           RuleFor(p => p.ShipName).NotEmpty();
           RuleFor(p => p.Freight).LessThan(100);
           RuleFor(p => p.ShipName).Must(StartWithA);
       }

       public bool StartWithA(string arg)
       {
           return arg.StartsWith("A");
       }
    }
}
