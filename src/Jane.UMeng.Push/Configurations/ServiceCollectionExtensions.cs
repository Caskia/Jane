using Jane.Push;
using Jane.UMeng.Push;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUMengPush(this IServiceCollection services, Action<UMengPushOptions> action = null)
        {
            services.Configure<UMengPushOptions>(JaneConfiguration.Instance.Root.GetSection("UMeng:Push"));
            if (action != null)
            {
                services.Configure(action);
            }

            var settings = new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(UMengPushService.JsonSerializerSettings)
            };
            services.AddRefitClient<IUMengPushApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(UMengPushService.ApiUrl));

            services.AddSingleton<IPushService, UMengPushService>();

            return services;
        }
    }
}