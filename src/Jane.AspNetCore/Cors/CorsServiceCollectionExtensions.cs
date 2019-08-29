using Jane.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Jane.AspNetCore.Cors
{
    public static class CorsServiceCollectionExtensions
    {
        public static void AddCorsPolicy(this IServiceCollection services, IConfigurationRoot appConfiguration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyNames.DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
                //options.AddPolicy(CorsPolicyNames.CorsAllPolicyName, builder =>
                //{
                //    builder.AllowAnyOrigin()
                //           .AllowAnyHeader()
                //           .AllowAnyMethod()
                //           .AllowCredentials();
                //});
            });
        }
    }
}