using Microsoft.EntityFrameworkCore;

namespace Jh.EfCoreRepository.Interfaces
{
    public interface IDataContext
    {
        public DbContext Context { get; }
    }
}
