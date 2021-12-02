using Jane.Netease.Im;
using Jane.Netease.Im.Handlers;
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
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
            };
            services.AddSingleton<NeteaseAuthorizationMessageHandler>();
            services.AddRefitClient<INeteaseImApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.netease.im/nimserver/"))
                    .AddHttpMessageHandler<NeteaseAuthorizationMessageHandler>();

            services.AddSingleton<INeteaseImService, NeteaseImService>();

            return services;
        }
    }
}