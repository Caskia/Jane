using Jane.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJaneAspNetCore(this IServiceCollection services)
        {
            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Configure MVC
            services.Configure<MvcOptions>(options =>
            {
                options.ConfigureJaneMvcOptions(services);
            });
        }
    }
}