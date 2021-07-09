using Jane.Agora.Rtc;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAgoraRtc(this IServiceCollection services, Action<AgoraRtcOptions> action = null)
        {
            var section = JaneConfiguration.Instance.Root.GetSection("Agora:Rtc");
            services.Configure<AgoraRtcOptions>(section);
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IAgoraRtcService, AgoraRtcService>();

            return services;
        }
    }
}