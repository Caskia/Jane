using Microsoft.AspNetCore.Builder;

namespace Jane.AspNetCore.Authentication.Cookie
{
    public static class CookieMiddleware
    {
        public static IApplicationBuilder UseCookieMiddleware(this IApplicationBuilder app, string authenticationScheme)
        {
            return app.UseAuthenticationMiddleware(authenticationScheme);
        }
    }
}