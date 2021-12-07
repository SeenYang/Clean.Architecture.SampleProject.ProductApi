using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SampleProject.Application;

namespace Aspnet.Webapi.Helpers
{
    public class WebApiHealthCheckService : IHealthCheck
    {
        private readonly IHealthCheckService _healthCheckService;

        public WebApiHealthCheckService(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = await _healthCheckService.IsDatabaseReady() &&
                                           await _healthCheckService.IsMessageQueueReady();

            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("A healthy result.");
            }

            return new HealthCheckResult(context.Registration.FailureStatus,
                "An unhealthy result.");
        }
    }
}