using APIPlayground.Utilities.Swagger;
using APIPlaygroundBusiness;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace APIPlayground
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // configure API versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddControllers();

            ConfigureSwagger(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<Randomizer>().As<IRandomizer>();
            builder.RegisterType<BusinessCalculator>().As<IBusinessCalculator   >();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "APIPlayground API V1.0");
                    c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "APIPlayground API V1.1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0",
                    new OpenApiInfo
                    {
                        Version = "v1.0",
                        Title = "v1.0 API",
                        Description = "v1.0 API Description",
                    });

                options.SwaggerDoc("v1.1",
                    new OpenApiInfo
                    {
                        Version = "v1.1",
                        Title = "v1.1 API",
                        Description = "v1.1 API Description",
                    });

                // apply api-version header filter
                options.OperationFilter<ApiVersionHeaderFilter>();

                // add routes to the right Swagger doc version
                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    var toReturn = versions.Any(v => $"v{v}" == version) && (!maps.Any() || maps.Any(v => $"v{v}" == version));

                    return toReturn;
                });
            });

        }
    }
}
