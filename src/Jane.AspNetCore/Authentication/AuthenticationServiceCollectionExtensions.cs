using Jane.AspNetCore.Authentication.JwtBearer;
using Jane.Extensions;
using Jane.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.AspNetCore.Authentication
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static AuthenticationBuilder ConfigureJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration, Action<TokenAuthConfiguration> setupAction = null)
        {
            var tokenAuthConfig = ConfigJwtTokenAuth(configuration, setupAction);
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
                         OnMessageReceived = QueryStringTokenResolver,
                         OnAuthenticationFailed = context =>
                         {
                             LogHelper.Logger.Warn($"OnAuthenticationFailed, context: {JsonConvert.SerializeObject(context)}");
                             return Task.CompletedTask;
                         },
                         OnTokenValidated = context =>
                         {
                             LogHelper.Logger.Warn($"OnTokenValidated, result: {JsonConvert.SerializeObject(context.Result)}");

                             LogHelper.Logger.Warn($"claims count {context.HttpContext.User.Claims.Count()}");

                             return Task.CompletedTask;
                         }
                     };
                 });
        }

        private static TokenAuthConfiguration ConfigJwtTokenAuth(IConfiguration configuration, Action<TokenAuthConfiguration> setupAction = null)
        {
            var tokenAuthConfig = new TokenAuthConfiguration();

            if (!configuration["JwtBearer:SecurityKey"].IsNullOrEmpty())
            {
                tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtBearer:SecurityKey"]));
            }

            if (!configuration["JwtBearer:CspKey"].IsNullOrEmpty())
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.ImportCspBlob(Convert.FromBase64String(configuration["JwtBearer:CspKey"]));
                tokenAuthConfig.SecurityKey = new RsaSecurityKey(rsa);
            }

            tokenAuthConfig.Issuer = configuration["JwtBearer:Issuer"];
            tokenAuthConfig.Audience = configuration["JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(Convert.ToInt32(configuration["JwtBearer:ExpirationDays"]));

            setupAction?.Invoke(tokenAuthConfig);

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

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}