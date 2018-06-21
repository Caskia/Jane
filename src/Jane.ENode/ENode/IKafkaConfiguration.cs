using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public interface IKafkaConfiguration
    {
        string BrokerAddresses { get; set; }

        List<IPEndPoint> BrokerEndPoints { get; }
    }
}