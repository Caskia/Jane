using Jane.AspNetCore.Logging;
using Jane.AspNetCore.Runtime.Session;
using Jane.Runtime.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ECommonConfiguration = ECommon.Configurations.Configuration;
using EENode = ENode;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class AspNetCore extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static ECommonConfiguration UseECommonAspNetCore(
            this ECommonConfiguration configuration,
            IServiceCollection services,
            out IServiceProvider serviceProvider
            )
        {
            configuration.SetDefault<ILogger, JaneMsLoggerAdapter>();
            configuration.SetDefault<IPrincipalAccessor, AspNetCorePrincipalAccessor>();

            serviceProvider = services.AddECommon(configuration);

            return configuration;
        }

        public static EENode.Configurations.ENodeConfiguration UseENodeAspNetCore(
            this EENode.Configurations.ENodeConfiguration configuration,
            IServiceCollection services,
            out IServiceProvider serviceProvider
            )
        {
            configuration.GetCommonConfiguration()
                  .UseECommonAspNetCore(services, out serviceProvider);
            return configuration;
        }
    }
}