using Jane.Netease.Im;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Refit;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNeteaseIm(this IServiceCollection services, Action<NeteaseImOptions> action = null)
        {
            services.Configure<NeteaseImOptions>(JaneConfiguration.Instance.Root.GetSection("Netease:Im"));
            if (action != null)
            {
                services.Configure(action);
            }

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var settings = new RefitSettings()
            {
                ContentSerializer = new JsonContentSerializer(jsonSerializerSettings)
            };
            services.AddRefitClient<INeteaseImApi>(settings)
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.netease.im/nimserver/"));

            services.AddSingleton<INeteaseImService, NeteaseImService>();

            return services;
        }
    }
}