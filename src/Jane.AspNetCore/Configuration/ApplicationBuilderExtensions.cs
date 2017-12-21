using Jane.AspNetCore.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jane.AspNetCore.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseJane(this IApplicationBuilder app)
        {
            app.UseJaneLoggerFactory();
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
    }
}