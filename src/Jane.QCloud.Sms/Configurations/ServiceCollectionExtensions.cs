using Jane.QCloud.Sms;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All))
                })
            };
            services.AddRefitClient<IQCloudSmsApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://yun.tim.qq.com"));

            services.AddSingleton<IQCloudSmsTemplateService, QCloudSmsTemplateService>(sp =>
            {
                return new QCloudSmsTemplateService(templateCodePairs);
            });
            services.AddSingleton<QCloudSmsService, QCloudSmsService>();
            services.AddSingleton<QCloudCombinationSmsService, QCloudCombinationSmsService>();

            return services;
        }
    }
}