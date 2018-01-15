using Autofac.Extensions.DependencyInjection;
using Jane.Autofac;
using Jane.Dependency;
using Jane.Json.Converters;
using Jane.Json.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Converters;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddJane(this IServiceCollection services)
        {
            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            if (ObjectContainer.Current is AutofacObjectContainer)
            {
                var objectContainer = ObjectContainer.Current as AutofacObjectContainer;

                objectContainer.ContainerBuilder.Populate(services);
                objectContainer.Build();

                return new AutofacServiceProvider(objectContainer.Container);
            }
            else
            {
                throw new JaneException("Current container not support!");
            }
        }

        public static void ConfigureMvcJsonOptions(this IServiceCollection services)
        {
            services.Configure<MvcJsonOptions>(options =>
            {
                options.SerializerSettings.ContractResolver = new CustomPropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new LongConverter());
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }
    }
}