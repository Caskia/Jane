using Jane.AspNetCore.ExceptionHandling;
using Jane.AspNetCore.Logging;
using Jane.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jane.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHostNameHeader(this IApplicationBuilder app)
        {
            app.UseMiddleware<HostNameMiddleware>();
        }

        public static void UseJane(this IApplicationBuilder app)
        {
            app.UseJaneLoggerFactory();
        }

        public static void UseJaneExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<JaneExceptionHandlingMiddleware>();
        }

        public static void UseJaneLoggerFactory(this IApplicationBuilder app)
        {
            var janeLoggerFactory = app.ApplicationServices.GetService<Jane.Logging.ILoggerFactory>();
            if (janeLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddJaneLogger(janeLoggerFactory);
        }

        public static void UseProcessingTimeHeader(this IApplicationBuilder app)
        {
            app.UseMiddleware<ProcessingTimeMiddleware>();
        }
    }
}