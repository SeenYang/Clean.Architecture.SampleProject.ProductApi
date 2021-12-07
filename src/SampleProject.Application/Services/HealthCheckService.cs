using System.Threading.Tasks;
using SampleProject.Domain.Interface;

namespace SampleProject.Application.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IMessageQueueClient _msgClient;
        private readonly IProductRepository _productRepo;

        public HealthCheckService(IMessageQueueClient msgClient, IProductRepository productRepo)
        {
            _msgClient = msgClient;
            _productRepo = productRepo;
        }

        public async Task<bool> IsMessageQueueReady()
        {
            return await _msgClient.CheckHealth();
        }

        public async Task<bool> IsDatabaseReady()
        {
            return await _productRepo.CheckHealth();
        }
    }
}