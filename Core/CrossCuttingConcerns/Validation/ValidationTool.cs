using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {

        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);

            var valResult = validator.Validate(context);

            if (!valResult.IsValid)
            {
                throw new ValidationException(valResult.Errors);
            }

        }
    }
}
