using Autofac.Extensions.DependencyInjection;
using Jane.Dependency;
using System;

namespace Jane.Autofac
{
    public static class ServiceProviderExtensions
    {
        public static void PopulateJaneDIContainer(this IServiceProvider serviceProvider)
        {
            ObjectContainer.Current.SetContainer(serviceProvider.GetAutofacRoot());
        }
    }
}