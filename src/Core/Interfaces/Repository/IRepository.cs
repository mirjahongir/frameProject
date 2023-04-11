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
        ValueTask<T> GetAsync(TKey id, CancellationToken? token=null);
        ValueTask<IQueryable<T>> GetAllAsync(CancellationToken? token = null);
        ValueTask<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken? token = null);
        ValueTask AddAsync(T model, CancellationToken? token = null);
        ValueTask AddRangeAsync(IEnumerable<T> models, CancellationToken? token = null);
        ValueTask RemoveAsync(T model, CancellationToken? token = null);
        ValueTask<T> RemoveAsync(TKey id, CancellationToken? token = null);
        ValueTask RemoveRangeAsync(IEnumerable<T> models, CancellationToken? token = null);
        ValueTask UpdateAsync(T model, CancellationToken? token = null);
    }


}
