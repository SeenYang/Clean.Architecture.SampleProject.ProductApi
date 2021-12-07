using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Domain.Interface;
using SampleProject.Infra.Adapter;
using SampleProject.Infra.Entity;
using SampleProject.Infra.Interface;

namespace SampleProject.Infra.IoC
{
    public static class AddDbContextExtension
    {
        public static void AddProductDbContext(this IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddDbContext<IProductDbContext, ProductDbContext>(cfg => { cfg.UseNpgsql(""); });
        }
    }
}