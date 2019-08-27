using Jane.AspNetCore.Logging;
using Jane.AspNetCore.Mvc.Validation;
using Jane.AspNetCore.Runtime.Session;
using Jane.Dependency;
using Jane.Runtime.Session;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class AspNetCore extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static Configuration UseAspNetCore(this Configuration configuration)
        {
            configuration.UseWeb();

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