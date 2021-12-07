using System.Threading.Tasks;

namespace SampleProject.Application
{
    public interface IHealthCheckService
    {
        Task<bool> IsMessageQueueReady();
        Task<bool> IsDatabaseReady();
    }
}