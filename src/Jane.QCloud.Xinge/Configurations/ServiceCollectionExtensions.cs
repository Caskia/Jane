using Jane.Json.Microsoft;
using Jane.Push;
using Jane.QCloud.Xinge;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
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

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All)),
                    PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
                })
            };
            services.AddRefitClient<IQCloudXingeApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://openapi.xg.qq.com/v3"));

            services.AddSingleton<IPushService, QCloudXingePushService>();

            return services;
        }
    }
}