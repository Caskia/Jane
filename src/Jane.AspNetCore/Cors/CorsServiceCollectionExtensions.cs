using Jane.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Jane.AspNetCore.Cors
{
    public static class CorsServiceCollectionExtensions
    {
        public static void AddCorsPolicy(this IServiceCollection services, IConfigurationRoot _appConfiguration)
        {
            services.TryAdd(ServiceDescriptor.Transient<ICorsService, WildCardCorsService>());
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyNames.DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        .WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
                options.AddPolicy(CorsPolicyNames.CorsAllPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
        }
    }
}