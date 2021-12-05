using Microsoft.Extensions.DependencyInjection;
using SampleProject.Domain.Interface;
using SampleProject.Infra.Adapter;

namespace SampleProject.Infra.IoC
{
    public static class AddMsgClientExtension
    {
        public static void AddMsgClient(this IServiceCollection service)
        {
            service.AddSingleton<IMessageQueueClient, ProductMessageQueueClient>();
        }
    }
}