using Jh.EfCoreRepository.Interfaces;
using Jh.EfCoreRepository.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace Jh.EfCoreRepository
{
    public static class Start
    {
        public static void RegisterEntityRepository(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IEntityRepository<,>), typeof(EfRepository<,>));
        }
    }
}