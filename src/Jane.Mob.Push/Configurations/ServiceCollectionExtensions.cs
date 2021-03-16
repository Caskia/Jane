using Jane.Mob.Push;
using Jane.Push;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMobPush(this IServiceCollection services, Action<MobPushOptions> action = null)
        {
            services.Configure<MobPushOptions>(JaneConfiguration.Instance.Root.GetSection("Mob:Push"));
            if (action != null)
            {
                services.Configure(action);
            }

            var settings = new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(MobPushService.JsonSerializerSettings)
            };
            services.AddRefitClient<IMobPushApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api.push.mob.com"));

            services.AddSingleton<MobPushService, MobPushService>();
            services.AddSingleton<IPushService, MobCombinationPushService>();

            return services;
        }
    }
}