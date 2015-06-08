using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace WorkoutTracker.Api
{
    //http://codereview.stackexchange.com/questions/14226/generic-repository-and-unit-of-work-code

    public class Repository<T> : IRepository<T> where T : class
    {
        readonly DbContext _context;
        DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            DbEntityEntry entry = _context.Entry(entity);
            foreach (var propertyName in entry.OriginalValues.PropertyNames)
            {
                var original = entry.GetDatabaseValues().GetValue<object>(propertyName);
                var current = entry.CurrentValues.GetValue<object>(propertyName);
                if (!object.Equals(original, current))
                {
                    entry.Property(propertyName).IsModified = true;
                }
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }
    }
}