using System;
using System.Linq;
using System.Collections.Generic;
using Bd.Icm.DataAccess.Interfaces;

namespace Bd.Icm.DataAccess
{
    public interface IVersionedRepository<T> : IDisposable
        where T : class, IVersionedRecord
    {
        IQueryable<T> Fetch(int version);
        IEnumerable<T> FetchAll(int version);
        IEnumerable<T> Find(Func<T, bool> predicate);
        T Single(Func<T, bool> predicate, int version);
        T SingleOrDefault(Func<T, bool> predicate, int version);
        T First(Func<T, bool> predicate, int version);
        T FirstOrDefault(Func<T, bool> predicate, int version);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
