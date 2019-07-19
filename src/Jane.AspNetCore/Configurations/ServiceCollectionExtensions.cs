using Autofac.Extensions.DependencyInjection;
using Jane.Autofac;
using Jane.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddJane(this IServiceCollection services)
        {
            ObjectContainer.Build(services);

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
    }
}