using Jane.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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