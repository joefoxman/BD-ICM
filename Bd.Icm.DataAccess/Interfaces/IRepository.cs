using System;
using System.Linq;
using System.Collections.Generic;

namespace Bd.Icm.DataAccess
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> Fetch();
        IEnumerable<T> FetchAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T Single(Func<T, bool> predicate);
        T SingleOrDefault(Func<T, bool> predicate);
        T First(Func<T, bool> predicate);
        T FirstOrDefault(Func<T, bool> predicate);
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
