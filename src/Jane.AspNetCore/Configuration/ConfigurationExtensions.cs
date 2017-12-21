using Jane.AspNetCore.Logging;
using Microsoft.Extensions.DependencyInjection;
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
            var assemblies = new[]
           {
                Assembly.Load("Jane.AspNetCore")
            };
            configuration.RegisterAssemblies(assemblies);

            configuration.SetDefault<ILogger, JaneMsLoggerAdapter>();

            serviceProvider = services.AddJane();

            return configuration;
        }
    }
}