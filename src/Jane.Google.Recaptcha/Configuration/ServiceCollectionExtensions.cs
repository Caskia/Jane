using System;
using Jane.Captcha;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<GoogleRecaptchaV3Service, GoogleRecaptchaV3Service>();

            return services;
        }
    }
}