using Autofac;
using Jane.AspNetCore.Authentication;
using Jane.AspNetCore.Authentication.JwtBearer;
using Jane.AspNetCore.Cors;
using Jane.AspNetCore.Mvc;
using Jane.Configurations;
using Jane.Timing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace JaneWebHostExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseProcessingTimeHeader();

            app.UseHostNameHeader();

            app.UseJane();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseResponseCompression();

            app.UseJwtTokenMiddleware();

            app.UseRouting();

            app.UseCors(CorsPolicyNames.DefaultCorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            JaneConfiguration.Instance.CreateAutoMapperMappings();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var _bussinessAssemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };

            var autoMapperConfigurationAssemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };

            JaneConfiguration.Instance
                .UseAutofac(builder)
                .RegisterCommonComponents()
                .RegisterAssemblies(_bussinessAssemblies)
                .UseAspNetCore()
                .UseLog4Net()
                .UseClockProvider(ClockProviders.Utc)
                .UseAutoMapper(autoMapperConfigurationAssemblies)
                .RegisterUnhandledExceptionHandler();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure Jane
            JaneConfiguration.Create();
            services.AddJaneAspNetCore();

            //Compression
            services.AddResponseCompression();

            //Configure CORS for application
            services.AddCorsPolicy(JaneConfiguration.Instance.Root);

            services.AddControllers()
                .AddJaneJsonOptions();

            //Configure Auth
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .ConfigureJwtBearer(JaneConfiguration.Instance.Root);
        }
    }
}