using Autofac;
using ECommon.Configurations;
using ECommon.JsonNet;
using ECommon.Serializing;
using Jane.Autofac;
using Jane.Dependency;
using Jane.ENode;
using Jane.Extensions;
using Jane.Json.Converters;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;
using ECommonConfiguration = ECommon.Configurations.Configuration;
using EENode = ENode;

namespace Jane.Configurations
{
    public static class ConfigurationExtensions
    {
        public static ECommonConfiguration BuildECommonContainer(this ECommonConfiguration configuration, IServiceCollection services = null)
        {
            if (ObjectContainer.Current is AutofacObjectContainer && ECommon.Components.ObjectContainer.Current is ECommon.Autofac.AutofacObjectContainer)
            {
                var ecommonObjectContainer = ECommon.Components.ObjectContainer.Current as ECommon.Autofac.AutofacObjectContainer;
                //integrate with microsoft di.
                if (services != null)
                {
                    ecommonObjectContainer.ContainerBuilder.Populate(services);
                }
                ecommonObjectContainer.Build();

                var objectContainer = new AutofacObjectContainer(ecommonObjectContainer.ContainerBuilder);
                objectContainer.SetContainer(ecommonObjectContainer.Container);
                ObjectContainer.SetContainer(objectContainer);
            }
            else
            {
                throw new JaneException("Current container not support!");
            }

            return configuration;
        }

        public static EENode.Configurations.ENodeConfiguration BuildENodeContainer(this EENode.Configurations.ENodeConfiguration configuration, IServiceCollection services = null)
        {
            configuration.GetCommonConfiguration()
                .BuildECommonContainer(services);
            return configuration;
        }

        public static ECommonConfiguration CreateECommon(this Configuration configuration)
        {
            return ECommonConfiguration.Create()
              .UseECommonAutofac()
              .RegisterCommonComponents()
              .UseLog4Net()
              .UseECommonJsonNet();
        }

        public static Configuration LoadENodeConfiguration(this Configuration configuration)
        {
            var enodeConfiguration = new ENodeConfiguration();
            if (!configuration.Root["ENode:AggregateSnapshotConnectionString"].IsNullOrEmpty())
            {
                enodeConfiguration.AggregateSnapshotConnectionString = configuration.Root["ENode:AggregateSnapshotConnectionString"];
            }

            if (!configuration.Root["ENode:AggregateSnapshotDatabaseName"].IsNullOrEmpty())
            {
                enodeConfiguration.AggregateSnapshotDatabaseName = configuration.Root["ENode:AggregateSnapshotDatabaseName"];
            }

            if (!configuration.Root["ENode:EventStoreConnectionString"].IsNullOrEmpty())
            {
                enodeConfiguration.EventStoreConnectionString = configuration.Root["ENode:EventStoreConnectionString"];
            }

            if (!configuration.Root["ENode:EventStoreDatabaseName"].IsNullOrEmpty())
            {
                enodeConfiguration.EventStoreDatabaseName = configuration.Root["ENode:EventStoreDatabaseName"];
            }

            if (!configuration.Root["ENode:LockServiceConnectionString"].IsNullOrEmpty())
            {
                enodeConfiguration.LockServiceConnectionString = configuration.Root["ENode:LockServiceConnectionString"];
            }

            if (!configuration.Root["ENode:LockServiceDatabaseId"].IsNullOrEmpty())
            {
                if (!int.TryParse(configuration.Root["ENode:LockServiceDatabaseId"], out int databaseId))
                {
                    databaseId = -1;
                }

                enodeConfiguration.LockServiceDatabaseId = databaseId;
            }

            return configuration.SetDefault<IENodeConfiguration, ENodeConfiguration>(enodeConfiguration);
        }

        public static Configuration LoadEQueueConfiguration(this Configuration configuration)
        {
            var equeueConfiguration = new EQueueConfiguration();
            if (!configuration.Root["EQueue:BrokerAdminHost"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerAdminHost = configuration.Root["EQueue:BrokerAdminHost"];
            }
            if (!configuration.Root["EQueue:BrokerAdminPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerAdminPort = Convert.ToInt32(configuration.Root["EQueue:BrokerAdminPort"]);
            }
            if (!configuration.Root["EQueue:BrokerConsumerHost"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerConsumerHost = configuration.Root["EQueue:BrokerConsumerHost"];
            }
            if (!configuration.Root["EQueue:BrokerConsumerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerConsumerPort = Convert.ToInt32(configuration.Root["EQueue:BrokerConsumerPort"]);
            }
            if (!configuration.Root["EQueue:BrokerProducerHost"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerProducerHost = configuration.Root["EQueue:BrokerProducerHost"];
            }
            if (!configuration.Root["EQueue:BrokerProducerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerProducerPort = Convert.ToInt32(configuration.Root["EQueue:BrokerProducerPort"]);
            }
            if (!configuration.Root["EQueue:BrokerStorePath"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerStorePath = configuration.Root["EQueue:BrokerStorePath"];
            }
            if (!configuration.Root["EQueue:BrokerName"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerName = configuration.Root["EQueue:BrokerName"];
            }
            if (!configuration.Root["EQueue:BrokerGroupName"].IsNullOrEmpty())
            {
                equeueConfiguration.BrokerGroupName = configuration.Root["EQueue:BrokerGroupName"];
            }
            if (!configuration.Root["EQueue:NameServerAddress"].IsNullOrEmpty())
            {
                equeueConfiguration.NameServerAddress = configuration.Root["EQueue:NameServerAddress"];
            }
            if (!configuration.Root["EQueue:NameServerPort"].IsNullOrEmpty())
            {
                equeueConfiguration.NameServerPort = Convert.ToInt32(configuration.Root["EQueue:NameServerPort"]);
            }

            return configuration.SetDefault<IEQueueConfiguration, EQueueConfiguration>(equeueConfiguration);
        }

        public static Configuration LoadKafkaConfiguration(this Configuration configuration)
        {
            var kafkaConfiguration = new KafkaConfiguration();
            if (!configuration.Root["Kafka:BrokerAddresses"].IsNullOrEmpty())
            {
                kafkaConfiguration.BrokerAddresses = configuration.Root["Kafka:BrokerAddresses"];
            }

            return configuration.SetDefault<IKafkaConfiguration, KafkaConfiguration>(kafkaConfiguration);
        }

        private static ECommonConfiguration UseECommonAutofac(this ECommonConfiguration configuration)
        {
            if (ObjectContainer.Current is AutofacObjectContainer)
            {
                var objectContainer = ObjectContainer.Current as AutofacObjectContainer;
                configuration.UseAutofac(objectContainer.ContainerBuilder);
            }
            else
            {
                throw new JaneException("Current container not support!");
            }

            return configuration;
        }

        private static ECommonConfiguration UseECommonJsonNet(this ECommonConfiguration configuration)
        {
            var serializer = new NewtonsoftJsonSerializer();
            serializer.Settings.Converters.Add(new LongConverter());
            serializer.Settings.Converters.Add(new StringEnumConverter());
            configuration.SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>(serializer);
            return configuration;
        }
    }
}