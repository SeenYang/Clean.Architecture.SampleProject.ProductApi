using Microsoft.Extensions.DependencyInjection;
using SampleProject.Application.Services;

namespace SampleProject.Application.IoC
{
    public static class AddServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
        }
    }
}