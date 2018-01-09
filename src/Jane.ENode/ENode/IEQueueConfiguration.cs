using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public interface IEQueueConfiguration
    {
        int BrokerAdminPort { get; set; }

        int BrokerConsumerPort { get; set; }

        int BrokerProducerPort { get; set; }

        string BrokerStorePath { get; set; }

        string NameServerAddress { get; set; }

        List<IPEndPoint> NameServerEndPoints { get; }

        int NameServerPort { get; set; }
    }
}