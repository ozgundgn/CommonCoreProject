using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.AspectsOriented.Autofac.Validation
{
    public class ValidationAspect : Modellnterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir.");
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entitiesType = _validatorType.BaseType.GetGenericArguments()[0]; // IValidator da türeyene classın oluşturulduğu base type
            var entities = invocation.Arguments.Where(c => c.GetType() == entitiesType);

            foreach (var entity in entities) //model içindeki her bir propertinin doğruluğunu kontrol edecek
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
