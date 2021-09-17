using Jane.Sms;
using Jane.Twilio.Sms;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider UseTwilioSms(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ISmsSupplierManager>()
                .AddSupplier(new SmsSupplier
                {
                    CountryCode = "*",
                    Service = serviceProvider.GetService<TwilioSmsService>()
                });

            return serviceProvider;
        }
    }
}