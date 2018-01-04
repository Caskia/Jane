using Autofac;
using ECommon.Configurations;
using Jane.Autofac;
using Jane.Dependency;
using Jane.ENode;
using Jane.Extensions;
using System;
using ECommonConfiguration = ECommon.Configurations.Configuration;
using EENode = ENode;

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

        public static EENode.Configurations.ENodeConfiguration BuildENodeContainer(this EENode.Configurations.ENodeConfiguration configuration)
        {
            configuration.GetCommonConfiguration()
                .BuildECommonContainer();
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
            if (!configuration.Root["EQueue:BrokerAdminPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerAdminPort = Convert.ToInt32(configuration.Root["EQueue:BrokerAdminPort"]);
            }
            if (!configuration.Root["EQueue:BrokerConsumerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerConsumerPort = Convert.ToInt32(configuration.Root["EQueue:BrokerConsumerPort"]);
            }
            if (!configuration.Root["EQueue:BrokerProducerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerProducerPort = Convert.ToInt32(configuration.Root["EQueue:BrokerProducerPort"]);
            }
            if (!configuration.Root["EQueue:BrokerStorePath"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerStorePath = configuration.Root["EQueue:BrokerStorePath"];
            }
            if (!configuration.Root["EQueue:NameServerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.NameServerPort = Convert.ToInt32(configuration.Root["EQueue:NameServerPort"]);
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