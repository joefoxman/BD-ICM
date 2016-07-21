using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Bd.Icm.Core;
using Bd.Icm.DataAccess.Database;
using Bd.Icm.DataAccess.Interfaces;
using EntityState = System.Data.Entity.EntityState;

namespace Bd.Icm.DataAccess
{
    public abstract class VersionedRepository<T> : 
        IVersionedRepository<T>
        where T : class, IVersionedRecord, IAuditedRecord, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected VersionedRepository()
            : this(new BdIcmEntities())
        {

        }

        protected VersionedRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private int GetNextVersion(int modifiedBy)
        {
            using (var repository = new DbVersionRepository())
            {
                return repository.GetNextVersion(modifiedBy);
            }
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var id = GetNewId(entity);
            entity.Id = id;
            var version = GetNextVersion(entity.ModifiedBy);
            using (var transaction = new TransactionScope())
            {
                entity.EffectiveFrom = version;
                entity.EffectiveTo = int.MaxValue;
                entity.ModificationType = (byte)ModificationType.Insert;
                _dbSet.Add(entity);
                _context.SaveChanges();
                transaction.Complete();
            }
            return entity;
        }

        protected abstract int GetNewId(T entity);

        protected virtual void OnEntityUpdate(T entity) { }
        protected virtual void OnEntityDelete(T entity) { }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var version = GetNextVersion(entity.ModifiedBy);
            using (var transaction = new TransactionScope())
            {
                var currentEntity = Fetch(int.MaxValue).Single(x => (x.Id == entity.Id) && (x.RowVersion == entity.RowVersion));
                currentEntity.EffectiveTo = version;
                currentEntity.ModifiedBy = entity.ModifiedBy;
                currentEntity.ModifiedDate = entity.ModifiedDate;
                _dbSet.Attach(currentEntity);
                _context.Entry(currentEntity).State = EntityState.Modified;

                entity.EffectiveFrom = version;
                entity.EffectiveTo = int.MaxValue;
                OnEntityUpdate(entity);
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.ModificationType = (byte)ModificationType.Update;
                _dbSet.Add(entity);
                _context.SaveChanges();
                transaction.Complete();
                return entity;
            }
        }

        public T Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var version = GetNextVersion(entity.ModifiedBy);
            using (var transaction = new TransactionScope())
            {
                entity.EffectiveTo = version;
                entity.ModificationType = (byte)ModificationType.Delete;
                OnEntityDelete(entity);
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Complete();
                return entity;
            }
        }

        public IQueryable<T> Fetch(int version)
        {
            return _dbSet.Where(x =>
                (x.EffectiveTo == int.MaxValue && version == int.MaxValue) ||
                ((x.EffectiveFrom <= version) && (x.EffectiveTo > version)));
        }

        public IQueryable<T> Fetch()
        {
            return _dbSet;
        }

        public IEnumerable<T> FetchAll(int version)
        {
            return Fetch(version).AsEnumerable();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where<T>(predicate);
        }

        public T Single(Func<T, bool> predicate, int version)
        {
            return _dbSet.Where(x =>
                (x.EffectiveTo == int.MaxValue && version == int.MaxValue) ||
                ((x.EffectiveFrom <= version) && (x.EffectiveTo > version)))
                .Single<T>(predicate);
        }

        public T SingleOrDefault(Func<T, bool> predicate, int version)
        {
            return _dbSet.Where(x => 
                (x.EffectiveTo == int.MaxValue && version == int.MaxValue) ||
                ((x.EffectiveFrom <= version) && (x.EffectiveTo > version)))
                .SingleOrDefault(predicate);
        }

        public T First(Func<T, bool> predicate, int version)
        {
            return _dbSet.Where(x =>
                (x.EffectiveTo == int.MaxValue && version == int.MaxValue) ||
                ((x.EffectiveFrom <= version) && (x.EffectiveTo > version)))
                .First<T>(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate, int version)
        {
            return _dbSet.Where(x =>
                (x.EffectiveTo == int.MaxValue && version == int.MaxValue) ||
                ((x.EffectiveFrom <= version) && (x.EffectiveTo > version)))
                .FirstOrDefault<T>(predicate);
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
