using Jane.QCloud.Cos;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudCos(this IServiceCollection services, Action<QCloudCosOptions> action = null)
        {
            services.Configure<QCloudCosOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud"));
            services.Configure<QCloudCosOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Cos"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IQCloudCosService, QCloudCosService>();

            return services;
        }
    }
}