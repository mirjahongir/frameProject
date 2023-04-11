using System.Threading.Tasks;
using System.Threading;

namespace Jh.Web.CronJob.Interfaces
{
    public interface IMyScopedService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
