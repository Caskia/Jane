using Jane.QCloud.Im;
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
        public static IServiceCollection AddQCloudIm(this IServiceCollection services, Action<QCloudMessagingOptions> action = null)
        {
            services.Configure<QCloudMessagingOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Im"));
            if (action != null)
            {
                services.Configure(action);
            }

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All))
                })
            };
            services.AddRefitClient<IQCloudMessagingApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://console.tim.qq.com"));

            services.AddSingleton<IQCloudMessagingService, QCloudMessagingService>();

            return services;
        }
    }
}