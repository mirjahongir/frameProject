using System.Linq.Expressions;
using Jh.Core.Interfaces.Repository;
using Jh.EfCoreRepository.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Jh.EfCoreRepository.Repository
{
    public class EfRepository<T, TKey> : IEntityRepository<T, TKey>//, IDisposable
        where T : class, IEntity<TKey>
        where TKey : struct
    {
        public EfRepository(DbContext context)
        {
            Context = context;
            var name = typeof(T).Name;

            Table = Context.Set<T>();
        }

        #region
        DbContext Context;
        protected DbSet<T> Table { get; }

        public void SaveChanges()
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
        public virtual void Add(T model, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.Add(model);
            SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> models, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.AddRange(models);
            SaveChanges();
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
        public virtual void Remove(T model, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.Remove(model);
            SaveChanges();
        }

        public virtual T Remove(TKey id, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return null;

            var model = GetAsync(id, token).Result;
            Table.Remove(model);
            SaveChanges();
            return model;
        }

        public virtual void RemoveRange(IEnumerable<T> models, CancellationToken? token = null)
        {
            if (CheckCancelToken(token)) return;
            Table.RemoveRange(models);
            SaveChanges();

        }
        #endregion

        #region Update
        public virtual void Update(T model, CancellationToken? token = null)
        {

            if (CheckCancelToken(token)) return;
            Context.Entry(model).State = EntityState.Modified;
            Table.Update(model);
            SaveChanges();

        }
        public virtual void UpdateRange(IEnumerable<T> models)
        {
            Context.Entry(models).State = EntityState.Modified;
            Table.UpdateRange(models);
            SaveChanges();
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
