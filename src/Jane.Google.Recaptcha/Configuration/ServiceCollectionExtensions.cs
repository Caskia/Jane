using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Jane.Captcha;
using Jane.Google.Recaptcha.Apis;
using Jane.Json.Microsoft;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddGoogleRecaptchaV3(this IServiceCollection services, Action<GoogleRecaptchaV3Options> action = null)
        {
            services.Configure<GoogleRecaptchaV3Options>(JaneConfiguration.Instance.Root.GetSection("Google:RecaptchaV3"));
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
            services.AddRefitClient<IGoogleRecaptchaV3Api>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.google.com"));

            services.AddSingleton<GoogleRecaptchaV3Service, GoogleRecaptchaV3Service>();

            return services;
        }
    }
}