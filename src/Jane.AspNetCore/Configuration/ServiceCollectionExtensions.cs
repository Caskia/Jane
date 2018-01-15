using Autofac.Extensions.DependencyInjection;
using Jane.Autofac;
using Jane.Dependency;
using Jane.Json.Converters;
using Jane.Json.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddJane(this IServiceCollection services)
        {
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