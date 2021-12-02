using Jane.RongCloud.Im;
using Jane.RongCloud.Im.Handlers;
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
        public static IServiceCollection AddRongCloudIm(this IServiceCollection services, Action<RongCloudImOptions> action = null)
        {
            var section = JaneConfiguration.Instance.Root.GetSection("RongCloud:Im");
            services.Configure<RongCloudImOptions>(section);
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
            services.AddSingleton<RongCloudAuthorizationMessageHandler>();
            services.AddRefitClient<IRongCloudImApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri($"https://{section[nameof(RongCloudImOptions.DataCenter)]}/"))
                    .AddHttpMessageHandler<RongCloudAuthorizationMessageHandler>();

            services.AddSingleton<IRongCloudImService, RongCloudImService>();

            return services;
        }
    }
}