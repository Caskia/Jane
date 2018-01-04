using Autofac;
using ECommon.Configurations;
using Jane.Autofac;
using Jane.Dependency;
using Jane.ENode;
using Jane.Extensions;
using System;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static ECommonConfiguration BuildECommonContainer(this ECommonConfiguration configuration)
        {
            ECommon.Components.ObjectContainer.Build();

            if (ObjectContainer.Current is AutofacObjectContainer && ECommon.Components.ObjectContainer.Current is ECommon.Autofac.AutofacObjectContainer)
            {
                var ecommonObjectContainer = ECommon.Components.ObjectContainer.Current as ECommon.Autofac.AutofacObjectContainer;
                ObjectContainer.SetContainer(new AutofacObjectContainer(ecommonObjectContainer.ContainerBuilder));

                var objectContainer = ObjectContainer.Current as AutofacObjectContainer;
                objectContainer.SetContainer(ecommonObjectContainer.Container);
            }

            return configuration;
        }

        public static ECommonConfiguration CreateECommon(this Configuration configuration)
        {
            return ECommonConfiguration.Create()
              .UseECommonAutofac()
              .RegisterCommonComponents()
              .UseLog4Net()
              .UseJsonNet();
        }

        public static Configuration LoadENodeConfiguration(this Configuration configuration)
        {
            var enodeConfiguration = new ENodeConfiguration();
            if (!configuration.Root["ENode:EventStoreConnectionString"].IsNullOrEmpty())
            {
                enodeConfiguration.EventStoreConnectionString = configuration.Root["ENode:EventStoreConnectionString"];
            }

            return configuration.SetDefault<IENodeConfiguration, ENodeConfiguration>(enodeConfiguration);
        }

        public static Configuration LoadEQueueConfiguration(this Configuration configuration)
        {
            var equeueConfiguration = new EQueueConfiguration();
            if (!configuration.Root["ENode:BrokerAdminPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerAdminPort = Convert.ToInt32(configuration.Root["ENode:BrokerAdminPort"]);
            }
            if (!configuration.Root["ENode:BrokerConsumerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerConsumerPort = Convert.ToInt32(configuration.Root["ENode:BrokerConsumerPort"]);
            }
            if (!configuration.Root["ENode:BrokerProducerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerProducerPort = Convert.ToInt32(configuration.Root["ENode:BrokerProducerPort"]);
            }
            if (!configuration.Root["ENode:BrokerStorePath"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerStorePath = configuration.Root["ENode:BrokerStorePath"];
            }
            if (!configuration.Root["ENode:NameServerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.NameServerPort = Convert.ToInt32(configuration.Root["ENode:NameServerPort"]);
            }

            return configuration.SetDefault<IEQueueConfiguration, EQueueConfiguration>(equeueConfiguration);
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