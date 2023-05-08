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
        public virtual async ValueTask AddAsync(T model, CancellationToken? token=null)
        {
            if (CheckCancelToken(token)) return;
            Table.Add(model);
            SaveChange();
        }

        public virtual async ValueTask AddRangeAsync(IEnumerable<T> models, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.AddRange(models);
            SaveChange();
        }
        #endregion

        #region Query
        public virtual async ValueTask<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return null;
            return Table.Where(predicate);
        }

        public virtual async ValueTask<T> GetAsync(TKey id, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return null;
            return Table.Find(id);
        }
        public T Get(TKey id)
        {
            return Table.Find(id);
        }
        public virtual async ValueTask<IQueryable<T>> GetAllAsync(CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return null;
            return Table;
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, CancellationToken? token = null)
        {

            if (CheckCancelToken(token)) return null;
            return Table.Where(predicate);
        }
        #endregion 

        #region Remove
        public virtual async ValueTask RemoveAsync(T model, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.Remove(model);
            SaveChange();
        }

        public virtual async ValueTask<T> RemoveAsync(TKey id, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return null;

            var model = await GetAsync(id, token);
            Table.Remove(model);
            SaveChange();
            return model;
        }

        public virtual async ValueTask RemoveRangeAsync(IEnumerable<T> models, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.RemoveRange(models);
            SaveChange();
        }
        #endregion

        #region Update
        public virtual async ValueTask UpdateAsync(T model, CancellationToken? token = null)
        {

            if (CheckCancelToken(token)) return;
            Context.Entry(model).State = EntityState.Modified;
            Table.Update(model);
            SaveChange();
        }
        #endregion

        public void RunSql(string sql, CancellationToken? token = null, params object[] param)
        {
            if (CheckCancelToken(token)) return;
            Context.Database.ExecuteSqlRaw(sql, param);
        }
        public IQueryable<T1> SqlQuery<T1>(FormattableString str, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) throw new ArgumentNullException(nameof(token));
            return Context.Database.SqlQuery<T1>(str);
        }
        public Tuple<bool, Exception> RunTransaction(Func<DbContext, DbSet<T>, bool> func, CancellationToken? token = null)
        {
            try
            {
                CheckCancelToken(token);
                using var transaction = Context.Database.BeginTransaction();
                var result = func(Context, Table);
                if (result)
                {
                    SaveChange();
                    transaction.Commit();
                }
                return new Tuple<bool, Exception>(result, null);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.Message);
                return new Tuple<bool, Exception>(false, ext);
            }

        }

        
    }
}
