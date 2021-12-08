using System.Reflection;
using Aspnet.Webapi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleProject.Application.IoC;
using SampleProject.Infra.IoC;
using SampleProject.Infra.Models;
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
            CommonConfigureServices(services);
            services.AddServices();
            var awsOptions = Configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsOptions);
            services.AddMsgClient(new AwsConfig{IsLocalMode = false});
            services.AddProductDbContext(new AwsConfig{IsLocalMode = false});
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            CommonConfigureServices(services);
            services.AddServices();
            var awsConfig = Configuration.GetSection("AwsConfig").Get<AwsConfig>();
            services.AddMsgClient(awsConfig);
            services.AddProductDbContext(awsConfig);
        }

        private static void CommonConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleProject.Webapi", Version = "v1" });
                // TODO: Swagger for mandatory headers.
                // c.OperationFilter<SwaggerCustomiseHeaderFilter>();
            });
            
            // TODO: CorrelationContext
            // services.AddCorrelationContext();

            // TODO: problem Details.
            // services.AddProblemDetails();

            // TODO: Fluent Validation
            // services.AddControllers()
            //     .AddFluentValidation(fv =>
            //         fv.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
            services.AddHealthChecks()
                .AddCheck<WebApiHealthCheckService>("WebApi_ServiceHealthCheck");
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