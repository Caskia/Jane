using Autofac.Extensions.DependencyInjection;
using ECommon.Autofac;
using ECommon.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddECommon(this IServiceCollection services, ECommonConfiguration configuration)
        {
            configuration.BuildECommonContainer(services);

            if (ObjectContainer.Current is AutofacObjectContainer)
            {
                var ecommonObjectContainer = ObjectContainer.Current as AutofacObjectContainer;

                return new AutofacServiceProvider(ecommonObjectContainer.Container);
            }
            else
            {
                throw new JaneException("Current container not support!");
            }
        }
    }
}