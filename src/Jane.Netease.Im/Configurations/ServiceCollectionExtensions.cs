using Jane.Netease.Im;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNeteaseIm(this IServiceCollection services, Action<NeteaseImOptions> action = null)
        {
            services.Configure<NeteaseImOptions>(JaneConfiguration.Instance.Root.GetSection("Netease:Im"));
            if (action != null)
            {
                services.Configure(action);
            }

            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRanges(UnicodeRanges.All);

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                Encoder = JavaScriptEncoder.Create(encoderSettings)
            };

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
            };
            services.AddRefitClient<INeteaseImApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.netease.im/nimserver/"));

            services.AddSingleton<INeteaseImService, NeteaseImService>();

            return services;
        }
    }
}