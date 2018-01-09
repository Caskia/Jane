using Jane.AspNetCore.Logging;
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

            serviceProvider = services.AddECommon();

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