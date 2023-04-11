using System.Linq.Expressions;
using Jh.Core.Interfaces.Repository;
using Jh.EfCoreRepository.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Jh.EfCoreRepository.Repository
{
    public class EfRepository<T, TKey> : IEntityRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : struct
    {
        public EfRepository(IDataContext context)
        {
            Context = context.Context;
            Table = Context.Set<T>();
        }

        #region
        DbContext Context;
        protected DbSet<T> Table { get; }
        public void SaveChange()
        {
            Context.SaveChanges();
        }
        public bool CheckCancelToken(CancellationToken? token)
        {
            if (token.HasValue && token.GetValueOrDefault().IsCancellationRequested)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Add
        public virtual async ValueTask AddAsync(T model, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return;
            Table.Add(model);
            SaveChange();
        }

        public virtual async ValueTask AddRangeAsync(IEnumerable<T> models, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return;
            Table.AddRange(models);
            SaveChange();
        }
        #endregion

        #region Query
        public virtual async ValueTask<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return null;
            return Table.Where(predicate);
        }

        public virtual async ValueTask<T> GetAsync(TKey id, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return null;
            return Table.Find(id);
        }

        public virtual async ValueTask<IQueryable<T>> GetAllAsync(CancellationToken? token)
        {
            if (CheckCancelToken(token)) return null;
            return Table;
        }
        #endregion 

        #region Remove
        public virtual async ValueTask RemoveAsync(T model, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return;
            Table.Remove(model);
            SaveChange();
        }

        public virtual async ValueTask<T> RemoveAsync(TKey id, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return null;

            var model = await GetAsync(id, token);
            Table.Remove(model);
            SaveChange();
            return model;
        }

        public virtual async ValueTask RemoveRangeAsync(IEnumerable<T> models, CancellationToken? token)
        {
            if (CheckCancelToken(token)) return;
            Table.RemoveRange(models);
            SaveChange();
        }
        #endregion

        #region Update
        public virtual async ValueTask UpdateAsync(T model, CancellationToken? token)
        {

            if (CheckCancelToken(token)) return;
            Context.Entry(model).State = EntityState.Modified;
            Table.Update(model);
            SaveChange();
        }
        #endregion
    }
}
