using Jane.Captcha;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceProviderExtensions
    {
        public const string ServiceName = "GoogleRecaptchaV3";

        public static IServiceProvider UseGoogleRecaptchaV3(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ICaptchaFactory>()
                .AddService(ServiceName, serviceProvider.GetService<GoogleRecaptchaV3Service>());

            return serviceProvider;
        }
    }
}