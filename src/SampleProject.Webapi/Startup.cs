using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleProject.Application.IoC;
using SampleProject.Infra.IoC;
using Serilog;

namespace Aspnet.Webapi
{
    public class Startup
    {
        private const string AllowLocalHostOrigins = "_allowLocalHostOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleProject.Webapi", Version = "v1" });
                // TODO: Swagger for mandatory headers.
                // c.OperationFilter<SwaggerCustomiseHeaderFilter>();
            });

            services.AddServices();
            services.AddMsgClient();
            services.AddProductDbContext();

            // TODO: CorrelationContext
            // services.AddCorrelationContext();

            // TODO: problem Details.
            // services.AddProblemDetails();

            // TODO: Fluent Validation
            // services.AddControllers()
            //     .AddFluentValidation(fv =>
            //         fv.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
            // TODO: Health Check
            // services.AddHealthChecks().AddCheck<PaymentsHealthCheckService>("check_eventbus_status");

            // TODO: Add log settings.
            services.AddSingleton(provider => Log.Logger);

            services.AddCors(o =>
                o.AddPolicy(AllowLocalHostOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:2333", "https://localhost:2334")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleProject.Webapi v1"));
                app.UseCors(AllowLocalHostOrigins);
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseEndpoints(e =>
            {
                // More details about health check. here is the ref: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0
                // Docker also provide similar feature that can be used in matrix. https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0#docker-example
                e.MapHealthChecks("/health");
                e.MapControllers();
            });
        }
    }
}