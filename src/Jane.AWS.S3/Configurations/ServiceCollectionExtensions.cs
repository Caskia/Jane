using Jane.AWS.S3;
using Microsoft.Extensions.DependencyInjection;
using System;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAWSS3(this IServiceCollection services, Action<AWSS3Options> action = null)
        {
            services.Configure<AWSS3Options>(JaneConfiguration.Instance.Root.GetSection("AWS"));
            services.Configure<AWSS3Options>(JaneConfiguration.Instance.Root.GetSection("AWS:S3"));
            if (action != null)
            {
                services.Configure(action);
            }

            services.AddSingleton<IAWSS3Service, AWSS3Service>();

            return services;
        }
    }
}