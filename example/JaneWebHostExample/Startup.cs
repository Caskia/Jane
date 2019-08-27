using Autofac;
using Jane.AspNetCore.Authentication;
using Jane.AspNetCore.Authentication.JwtBearer;
using Jane.AspNetCore.Cors;
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
using Autofac.Extensions.DependencyInjection;
using Jane.Dependency;
using Jane.Autofac;
using System;

namespace JaneWebHostExample
{
    public class Startup
    {
        private Assembly[] _bussinessAssemblies;

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

            var container = app.ApplicationServices.GetAutofacRoot();
            if (!(ObjectContainer.Current is AutofacObjectContainer autofacObjectContainer))
                throw new InvalidOperationException($"Instance of {nameof(ObjectContainer)} is not of type {nameof(AutofacObjectContainer)}");
            autofacObjectContainer.SetContainer(container);

            JaneConfiguration.Instance.CreateAutoMapperMappings();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            InitializeJane(builder);
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
                .AddNewtonsoftJson();

            //Configure Auth
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .ConfigureJwtBearer(JaneConfiguration.Instance.Root);
        }

        private JaneConfiguration InitializeJane(ContainerBuilder containerBuilder)
        {
            _bussinessAssemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };

            var autoMapperConfigurationAssemblies = new[]
            {
                Assembly.GetExecutingAssembly()
            };

            return JaneConfiguration.Instance
                 .UseAutofac(containerBuilder)
                 .RegisterCommonComponents()
                 .RegisterAssemblies(_bussinessAssemblies)
                 .UseAspNetCore()
                 .UseLog4Net()
                 .UseClockProvider(ClockProviders.Utc)
                 .UseAutoMapper(autoMapperConfigurationAssemblies)
                 .RegisterUnhandledExceptionHandler();
        }
    }
}