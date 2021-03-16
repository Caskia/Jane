using Jane.Push;
using Jane.QCloud.Xinge;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudXinge(this IServiceCollection services, Action<QCloudXingeOptions> action = null)
        {
            services.Configure<QCloudXingeOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Xinge"));
            if (action != null)
            {
                services.Configure(action);
            }

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            };
            var settings = new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSerializerSettings)
            };
            services.AddRefitClient<IQCloudXingeApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://openapi.xg.qq.com/v3"));

            services.AddSingleton<IPushService, QCloudXingePushService>();

            return services;
        }
    }
}