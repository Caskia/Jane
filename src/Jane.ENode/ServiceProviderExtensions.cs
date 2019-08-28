using Autofac.Extensions.DependencyInjection;
using ECommon.Components;
using ECommon.Configurations;
using System;

namespace Jane.ENode
{
    public static class ServiceProviderExtensions
    {
        public static void PopulateENodeDIContainer(this IServiceProvider serviceProvider)
        {
            ObjectContainer.Current.SetContainer(serviceProvider.GetAutofacRoot());
        }
    }
}