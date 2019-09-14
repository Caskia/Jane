using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.QCloud.CKafka.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudCKafka(this IServiceCollection services, Action<QCloudCKafkaOptions> action = null)
        {
            services.Configure<QCloudCKafkaOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud"));
            services.Configure<QCloudCKafkaOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:CKafka"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IQCloudCKafkaService, QCloudCKafkaService>();

            return services;
        }
    }
}