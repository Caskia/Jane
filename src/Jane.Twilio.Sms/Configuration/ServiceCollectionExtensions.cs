using Jane.Sms;
using Jane.Twilio.Sms;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddTwilioSms(this IServiceCollection services, Action<TwilioSmsOptions> action = null)
        {
            services.Configure<TwilioSmsOptions>(JaneConfiguration.Instance.Root.GetSection("Twilio:Sms"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<ISupplySmsService, TwilioSmsService>();

            return services;
        }
    }
}