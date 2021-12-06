using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Application.Services;

namespace SampleProject.Application.IoC
{
    [ExcludeFromCodeCoverage]
    public static class AddServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
        }
    }
}