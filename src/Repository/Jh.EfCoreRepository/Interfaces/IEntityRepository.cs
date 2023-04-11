using Jh.Core.Interfaces.Repository;

namespace Jh.EfCoreRepository.Interfaces
{
    public interface IEntityRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : struct
    {
    
    }

}
