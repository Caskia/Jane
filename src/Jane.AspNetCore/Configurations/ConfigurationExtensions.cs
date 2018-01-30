using Jane.AspNetCore.Logging;
using Jane.AspNetCore.Mvc;
using Jane.AspNetCore.Mvc.Validation;
using Jane.AspNetCore.Runtime.Session;
using Jane.Dependency;
using Jane.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class AspNetCore extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static Configuration UseAspNetCore(this Configuration configuration, IServiceCollection services, out IServiceProvider serviceProvider)
        {
            configuration.UseAspNetCore();

            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Configure MVC
            services.Configure<MvcOptions>(options =>
            {
                options.ConfigureJaneMvcOptions(services);
            });

            //Configure Mvc Json
            services.ConfigureJaneMvcJsonOptions();

            serviceProvider = services.AddJane();

            return configuration;
        }

        public static Configuration UseAspNetCore(this Configuration configuration)
        {
            var assemblies = new[]
            {
                Assembly.Load("Jane.AspNetCore")
            };
            configuration.RegisterAssemblies(assemblies);

            configuration.SetDefault<ILogger, JaneMsLoggerAdapter>();
            configuration.SetDefault<IPrincipalAccessor, AspNetCorePrincipalAccessor>();

            configuration.SetDefault<MvcActionInvocationValidator, MvcActionInvocationValidator>(null, DependencyLifeStyle.Transient);

            return configuration;
        }
    }
}