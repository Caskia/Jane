using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace Jane.AspNetCore.Authentication
{
    public static class AuthenticationMiddleware
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app, string authenticationScheme)
        {
            return app.Use(async (ctx, next) =>
            {
                if (ctx.User.Identity?.IsAuthenticated != true)
                {
                    var result = await ctx.AuthenticateAsync(authenticationScheme);
                    if (result.Succeeded && result.Principal != null)
                    {
                        ctx.User = result.Principal;
                    }
                }

                await next();
            });
        }
    }
}