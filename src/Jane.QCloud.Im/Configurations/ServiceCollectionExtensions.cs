using Jane.QCloud.Im;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Refit;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudIm(this IServiceCollection services, Action<QCloudMessagingOptions> action = null)
        {
            services.Configure<QCloudMessagingOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Im"));
            if (action != null)
            {
                services.Configure(action);
            }

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var settings = new RefitSettings()
            {
                ContentSerializer = new JsonContentSerializer(jsonSerializerSettings)
            };
            services.AddRefitClient<IQCloudMessagingApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://console.tim.qq.com"));

            services.AddSingleton<IQCloudMessagingService, QCloudMessagingService>();

            return services;
        }
    }
}