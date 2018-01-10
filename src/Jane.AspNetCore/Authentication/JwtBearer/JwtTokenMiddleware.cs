using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;

namespace Jane.AspNetCore.Authentication.JwtBearer
{
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseAuthenticationMiddleware(JwtBearerDefaults.AuthenticationScheme);
        }
    }
}