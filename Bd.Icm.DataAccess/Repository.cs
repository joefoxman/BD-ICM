using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.DataAccess
{
    public class Repository<T> : 
        IRepository<T>
        where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository()
            : this(new BdIcmEntities())
        {

        }

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _dbSet.Remove(entity);
                int changes = _context.SaveChanges();

                if (changes == 0)
                    throw new OptimisticConcurrencyException();
            }
            catch (System.Data.OptimisticConcurrencyException)
            {
                throw new OptimisticConcurrencyException();
            }
        }

        public IQueryable<T> Fetch()
        {
            return _dbSet;
        }

        public IEnumerable<T> FetchAll()
        {
            return Fetch().AsEnumerable();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where<T>(predicate);
        }

        public T Single(Func<T, bool> predicate)
        {
            return _dbSet.Single<T>(predicate);
        }

        public T SingleOrDefault(Func<T, bool> predicate)
        {
            return _dbSet.SingleOrDefault<T>(predicate);
        }

        public T First(Func<T, bool> predicate)
        {
            return _dbSet.First<T>(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return _dbSet.FirstOrDefault<T>(predicate);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        #endregion
    }
}
