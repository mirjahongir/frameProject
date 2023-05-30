using Jh.MongoDbRepository.Interfaces;
using Jh.MongoDbRepository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Jh.MongoDbRepository
{
    public static class Start
    {
        public static void RegisterMongoRepository(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            //collection.AddScoped(typeof(MongoRepository<>), typeof(IMongoRepository<>));
        }
    }
}
