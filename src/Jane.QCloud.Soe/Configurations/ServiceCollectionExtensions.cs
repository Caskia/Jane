using Jane.QCloud.Soe;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudSoe(this IServiceCollection services, Action<QCloudSoeOptions> action = null)
        {
            services.Configure<QCloudSoeOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud"));
            services.Configure<QCloudSoeOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Soe"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IQCloudSoeService, QCloudSoeService>();

            return services;
        }
    }
}