using Jh.Core.Interfaces.Repository;

using Microsoft.EntityFrameworkCore;

namespace Jh.EfCoreRepository.Interfaces
{
    public interface IEntityRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : struct
    {
        void RunSql(string sql, CancellationToken? token = null, params object[] param);
        IQueryable<T1> SqlQuery<T1>(FormattableString str, CancellationToken? token = null);
        Tuple<bool, Exception> RunTransaction(Func<DbContext, DbSet<T>, bool> func, CancellationToken? token = null);
        void SaveChanges();
    }

}
