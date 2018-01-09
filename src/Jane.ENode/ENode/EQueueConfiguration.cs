using Jane.Utils;
using System;
using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public class EQueueConfiguration : IEQueueConfiguration
    {
        public int BrokerAdminPort { get; set; } = 10003;

        public int BrokerConsumerPort { get; set; } = 10002;

        public int BrokerProducerPort { get; set; } = 10001;

        public string BrokerStorePath { get; set; }

        public string NameServerAddress { get; set; }

        public List<IPEndPoint> NameServerEndPoints
        {
            get
            {
                var ipEndPoints = new List<IPEndPoint>();

                if (string.IsNullOrWhiteSpace(NameServerAddress))
                {
                    var defaultNameServer = new IPEndPoint(SocketUtils.GetLocalIPV4(), NameServerPort);
                    ipEndPoints.Add(defaultNameServer);
                }
                else
                {
                    var addressList = NameServerAddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var address in addressList)
                    {
                        var array = address.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        var endpoint = new IPEndPoint(IPAddress.Parse(array[0]), int.Parse(array[1]));
                        ipEndPoints.Add(endpoint);
                    }
                }
                return ipEndPoints;
            }
        }

        public int NameServerPort { get; set; } = 10000;
    }
}