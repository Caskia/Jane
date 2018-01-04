namespace Jane.ENode
{
    public interface IEQueueConfiguration
    {
        int BrokerAdminPort { get; set; }

        int BrokerConsumerPort { get; set; }

        int BrokerProducerPort { get; set; }

        string BrokerStorePath { get; set; }

        int NameServerPort { get; set; }
    }
}