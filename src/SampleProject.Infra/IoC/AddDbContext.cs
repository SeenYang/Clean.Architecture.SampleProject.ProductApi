using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SampleProject.Domain.Interface;
using SampleProject.Infra.Adapter;
using SampleProject.Infra.Interface;
using SampleProject.Infra.Models;
using SampleProject.Infra.Models.Entity;

namespace SampleProject.Infra.IoC
{
    public static class AddDbContextExtension
    {
        public static void AddProductDbContext(this IServiceCollection services, AwsConfig awsConfig)
        {
            IConfigurationRoot parameters;
            if (awsConfig.IsLocalMode)
            {
                // Using localstack
                var awsOptions = new AWSOptions
                {
                    Credentials = new BasicAWSCredentials(awsConfig.AccessKeyId, awsConfig.SecretAccessKey),
                    DefaultClientConfig =
                    {
                        ServiceURL = awsConfig.ServiceUrl,
                        AuthenticationRegion = awsConfig.Region
                    }
                };
                parameters = new ConfigurationBuilder()
                    .AddSystemsManager("samplewebapi", awsOptions)
                    .Build();
            }
            else
            {
                // Using aws container default setup.
                parameters = new ConfigurationBuilder()
                    .AddSystemsManager("samplewebapi")
                    .Build();
            }

            var conStr = parameters.GetSection("database").ToString();
            services.AddDbContext<IProductDbContext, ProductDbContext>(cfg => { cfg.UseNpgsql(conStr); });
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}