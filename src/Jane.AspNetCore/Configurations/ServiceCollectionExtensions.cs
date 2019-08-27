using Autofac.Extensions.DependencyInjection;
using Jane.AspNetCore.Mvc;
using Jane.Autofac;
using Jane.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddJane(this IServiceCollection services)
        {
            ObjectContainer.Populate(services);
            ObjectContainer.Build();

            if (ObjectContainer.Current is AutofacObjectContainer)
            {
                var objectContainer = ObjectContainer.Current as AutofacObjectContainer;

                return new AutofacServiceProvider(objectContainer.Container);
            }
            else
            {
                throw new JaneException("Current container not support!");
            }
        }

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

            //Configure Mvc Json
            services.ConfigureJaneMvcJsonOptions();
        }
    }
}