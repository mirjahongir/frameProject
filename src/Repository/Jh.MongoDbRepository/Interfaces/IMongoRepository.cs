using Jh.Core.Interfaces.Repository;

namespace Jh.MongoDbRepository.Interfaces
{
    public interface IMongoRepository<T> : IRepository<T, string>
        where T : class, IEntity<string>


    {
    }
}
