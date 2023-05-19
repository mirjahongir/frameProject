using Jh.MongoDbRepository.Interfaces;
using Jh.MongoDbRepository.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace Jh.MongoDbRepository
{
    public class Start
    {
        public static void Build(IServiceCollection collection)
        {
            collection.AddScoped(typeof(MongoRepository<>), typeof(IMongoRepository<>));
        }
    }
}
