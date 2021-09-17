using Jane.QCloud.Sms;
using Jane.Sms;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider UseQCloudSms(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ISmsSupplierManager>()
                .AddSupplier(new SmsSupplier
                {
                    CountryCode = "86",
                    Service = serviceProvider.GetService<QCloudCombinationSmsService>()
                });

            return serviceProvider;
        }
    }
}