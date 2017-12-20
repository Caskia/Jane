using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jane.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jane.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddJane(this IServiceCollection services)
        {
            if (ObjectContainer.Current is IContainer)
            {
                return new AutofacServiceProvider(ObjectContainer.Current as IContainer);
            }
            else
            {
                throw new Exception("Current container not support!");
            }
        }
    }
}