using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;

namespace Jh.Core.Interfaces.Repository
{
    public interface IRepository<T, TKey>
        where T : class, IEntity<TKey>
    {
        T Get(TKey id);
        ValueTask<T> GetAsync(TKey id, CancellationToken? token = null);
        ValueTask<IQueryable<T>> GetAllAsync(CancellationToken? token = null);
        ValueTask<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken? token = null);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, CancellationToken? token = null);
        void Add(T model, CancellationToken? token = null);
        void AddRange(IEnumerable<T> models, CancellationToken? token = null);
        void Remove(T model, CancellationToken? token = null);
        T Remove(TKey id, CancellationToken? token = null);
        void RemoveRange(IEnumerable<T> models, CancellationToken? token = null);
        void Update(T model, CancellationToken? token = null);
        void UpdateRange(IEnumerable<T> models);
        bool RemoveWhere(Expression<Func<T, bool>> predicate);
    }


}
