using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Domain.Interface;
using SampleProject.Infra.Adapter;
using SampleProject.Infra.Models;

namespace SampleProject.Infra.IoC
{
    public static class AddMsgClientExtension
    {
        public static void AddMsgClient(this IServiceCollection service, AwsConfig awsConfig)
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
                    .AddSystemsManager("messagequeue", awsOptions)
                    .Build();
            }
            else
            {
                parameters = new ConfigurationBuilder()
                    .AddSystemsManager("messagequeue")
                    .Build();
            }

            service.Configure<MsgClientOptions>(parameters.GetSection("messagequeue"));
            service.AddSingleton<IMessageQueueClient, ProductMessageQueueClient>();
        }
    }
}