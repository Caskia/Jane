using Jane.Agora.Rtc;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAgoraIm(this IServiceCollection services, Action<AgoraImOptions> action = null)
        {
            var section = JaneConfiguration.Instance.Root.GetSection("Agora:Rtc");
            services.Configure<AgoraImOptions>(section);
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IAgoraImService, AgoraImService>();

            return services;
        }
    }
}