using Autofac.Extensions.DependencyInjection;
using ECommon.Autofac;
using ECommon.Components;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddECommon(this IServiceCollection services)
        {
            if (ObjectContainer.Current is AutofacObjectContainer && Dependency.ObjectContainer.Current is Autofac.AutofacObjectContainer)
            {
                var ecommonObjectContainer = ObjectContainer.Current as AutofacObjectContainer;

                ecommonObjectContainer.ContainerBuilder.Populate(services);
                ecommonObjectContainer.Build();

                //regist jane object container
                Dependency.ObjectContainer.SetContainer(new Autofac.AutofacObjectContainer(ecommonObjectContainer.ContainerBuilder));

                var janeObjectContainer = Dependency.ObjectContainer.Current as Autofac.AutofacObjectContainer;
                janeObjectContainer.SetContainer(ecommonObjectContainer.Container);

                return new AutofacServiceProvider(ecommonObjectContainer.Container);
            }
            else
            {
                throw new BaseException("Current container not support!");
            }
        }
    }
}