using Autofac;
using ECommon.Configurations;
using Jane.Autofac;
using Jane.Dependency;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static ECommonConfiguration CreateECommon(this Configuration configuration)
        {
            return ECommonConfiguration.Create()
              .UseECommonAutofac()
              .RegisterCommonComponents()
              .UseLog4Net()
              .UseJsonNet();
        }

        private static ECommonConfiguration UseECommonAutofac(this ECommonConfiguration configuration)
        {
            ContainerBuilder containerBuilder;
            if (ObjectContainer.Current is AutofacObjectContainer)
            {
                var objectContainer = ObjectContainer.Current as AutofacObjectContainer;
                containerBuilder = objectContainer.ContainerBuilder;
                configuration.UseAutofac(containerBuilder);
            }
            else
            {
                throw new BaseException("Current container not support!");
            }

            return configuration;
        }
    }
}