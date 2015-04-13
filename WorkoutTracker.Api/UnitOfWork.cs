using System;
using System.Data.Entity;

namespace WorkoutTracker.Api
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> RepositoryFor<T>() where T : class;
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IRepository<T> RepositoryFor<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}