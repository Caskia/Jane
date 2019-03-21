using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jane.AspNetCore.Middlewares
{
    public class HostNameMiddleware
    {
        private readonly RequestDelegate _next;

        public HostNameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("X-Server", new[] { Environment.MachineName });
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}