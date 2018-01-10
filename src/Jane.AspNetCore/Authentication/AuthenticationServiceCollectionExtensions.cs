using Jane.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.AspNetCore.Authentication
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static AuthenticationBuilder ConfigureJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            var tokenAuthConfig = ConfigJwtTokenAuth(configuration);
            return builder.AddJwtBearer(options =>
                 {
                     options.Audience = tokenAuthConfig.Audience;

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         // The signing key must match!
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = tokenAuthConfig.SecurityKey,

                         // Validate the JWT Issuer (iss) claim
                         ValidateIssuer = true,
                         ValidIssuer = tokenAuthConfig.Issuer,

                         // Validate the JWT Audience (aud) claim
                         ValidateAudience = true,
                         ValidAudience = tokenAuthConfig.Audience,

                         // Validate the token expiry
                         ValidateLifetime = true,

                         // If you want to allow a certain amount of clock drift, set that here
                         ClockSkew = TimeSpan.Zero
                     };

                     options.Events = new JwtBearerEvents
                     {
                         OnMessageReceived = QueryStringTokenResolver
                     };
                 });
        }

        private static TokenAuthConfiguration ConfigJwtTokenAuth(IConfiguration configuration)
        {
            var tokenAuthConfig = new TokenAuthConfiguration();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = configuration["JwtBearer:Issuer"];
            tokenAuthConfig.Audience = configuration["JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(Convert.ToInt32(configuration["JwtBearer:ExpirationDays"]));

            JaneConfiguration.Instance.SetDefault<TokenAuthConfiguration, TokenAuthConfiguration>(tokenAuthConfig);
            return tokenAuthConfig;
        }

        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            //all to ajax request
            context.HttpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";

            if (!context.HttpContext.Request.Path.HasValue)
            {
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["access_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                //querystring value does not matches
                return Task.CompletedTask;
            }

            //Set auth token from querystring
            context.Token = qsAuthToken;
            return Task.CompletedTask;
        }
    }
}