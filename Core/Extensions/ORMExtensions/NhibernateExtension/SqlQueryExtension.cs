using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Core.Extensions.ORMExtensions.NhibernateExtension
{
    public static class SqlQueryExtension
    {
        public static ISQLQuery AddScalars<TModel>(this ISQLQuery sqlQuery, Expression<Func<TModel, object>> excludedProperties = null, Expression<Func<TModel, object>> onlyIncludedProperties = null) where TModel : class, new()
        {
            var excludedFields = new List<string>();
            if (excludedProperties != null)
            {
                var memberExpr = excludedProperties.Body as MemberExpression;
                if (memberExpr == null)
                {
                    var properties = excludedProperties.Body.Type.GetProperties();
                    excludedFields.AddRange(properties.Select(prop => prop.Name));
                }
                else if (memberExpr.Member.MemberType == MemberTypes.Property)
                {
                    excludedFields.Add(memberExpr.Member.Name);
                }
            }

            PropertyInfo[] includedProperties;
            Type modelType = typeof(TModel);

            if (onlyIncludedProperties != null)
            {
                includedProperties = onlyIncludedProperties.Body.Type.GetProperties();
            }
            else
            {
                includedProperties = modelType.GetProperties();
            }

            foreach (PropertyInfo propertyInfo in includedProperties)
            {
                if (excludedFields.Contains(propertyInfo.Name))
                {
                    continue;
                }

                if (!propertyInfo.CanWrite)
                {
                    continue;
                }

                Type property = propertyInfo.PropertyType;
                if (property.IsGenericType)
                {
                    property = propertyInfo.PropertyType.GetGenericArguments()[0];

                }

                IType type = NHibernateUtil.String;
                if (property == typeof(int))
                {
                    type = NHibernateUtil.Int32;
                }
                else if (property == typeof(DateTime))
                {
                    type = NHibernateUtil.DateTime;
                }
                else if (property == typeof(Double))
                {
                    type = NHibernateUtil.Double;
                }
                else if (property == typeof(Decimal))
                {
                    type = NHibernateUtil.Decimal;
                }
                else if (property == typeof(decimal))
                {
                    type = NHibernateUtil.Decimal;
                }
                else if (property == typeof(float))
                {
                    type = NHibernateUtil.Double;
                }
                else if (property == typeof(long))
                {
                    type = NHibernateUtil.Int64;
                }
                else if (property == typeof(TimeSpan))
                {
                    type = NHibernateUtil.DateTime;
                }
                else if (property == typeof(bool))
                {
                    type = NHibernateUtil.Boolean;
                }
                else if (property == typeof(object))
                {
                    type = NHibernateUtil.String;
                }
                else
                {
                    type = NHibernateUtil.GuessType(property);
                }

                sqlQuery.AddScalar(propertyInfo.Name, type);
            }

            return sqlQuery;
        }
    }
}
