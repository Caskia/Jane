using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public interface IEQueueConfiguration
    {
        IPEndPoint BrokerAdminEndPoint { get; }

        string BrokerAdminHost { get; set; }

        int BrokerAdminPort { get; set; }

        IPEndPoint BrokerConsumerEndPoint { get; }

        string BrokerConsumerHost { get; set; }

        int BrokerConsumerPort { get; set; }

        string BrokerGroupName { get; set; }

        string BrokerName { get; set; }

        IPEndPoint BrokerProducerEndPoint { get; }

        string BrokerProducerHost { get; set; }

        int BrokerProducerPort { get; set; }

        string BrokerStorePath { get; set; }

        string NameServerAddress { get; set; }
        List<IPEndPoint> NameServerEndPoints { get; }

        int NameServerPort { get; set; }
    }
}