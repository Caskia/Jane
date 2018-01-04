namespace Jane.ENode
{
    public interface IENodeConfiguration
    {
        int BrokerAdminPort { get; set; }

        int BrokerConsumerPort { get; set; }

        int BrokerProducerPort { get; set; }

        string BrokerStorePath { get; set; }

        string EventStoreConnectionString { get; set; }

        int NameServerPort { get; set; }
    }
}