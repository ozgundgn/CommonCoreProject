﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        long Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return default(T);
        }

        public virtual T Get(T entity)
        {
            return default(T);
        }
    }
}