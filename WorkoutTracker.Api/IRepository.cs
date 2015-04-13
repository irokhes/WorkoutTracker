using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WorkoutTracker.Api
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);

        T GetById(int id);

        void Insert(T entity);

        void Delete(T entity);

        void Delete(int id);
    }
}