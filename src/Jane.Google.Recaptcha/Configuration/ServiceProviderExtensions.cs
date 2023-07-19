using Jane.Captcha;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceProviderExtensions
    {
        public const string ServiceV2Name = "GoogleRecaptchaV2";
        public const string ServiceV3Name = "GoogleRecaptchaV3";


        public static IServiceProvider UseGoogleRecaptchaV2(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ICaptchaFactory>()
                .AddService(ServiceV2Name, serviceProvider.GetService<GoogleRecaptchaV2Service>());

            return serviceProvider;
        }

        public static IServiceProvider UseGoogleRecaptchaV3(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ICaptchaFactory>()
                .AddService(ServiceV3Name, serviceProvider.GetService<GoogleRecaptchaV3Service>());

            return serviceProvider;
        }
    }
}