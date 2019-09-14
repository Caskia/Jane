using Jane.QCloud.Sms;
using Jane.Sms;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQCloudSms(this IServiceCollection services, Dictionary<string, int> templateCodePairs, Action<QCloudSmsOptions> action = null)
        {
            services.Configure<QCloudSmsOptions>(JaneConfiguration.Instance.Root.GetSection("QCloud:Sms"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddRefitClient<IQCloudSmsApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://yun.tim.qq.com"));

            services.AddSingleton<IQCloudSmsTemplateService, QCloudSmsTemplateService>(sp =>
            {
                return new QCloudSmsTemplateService(templateCodePairs);
            });
            services.AddSingleton<QCloudSmsService, QCloudSmsService>();
            services.AddSingleton<ISmsService, QCloudCombinationSmsService>();

            return services;
        }
    }
}