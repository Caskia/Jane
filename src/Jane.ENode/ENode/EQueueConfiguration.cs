using Jane.Extensions;
using Jane.Utils;
using System;
using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public class EQueueConfiguration : IEQueueConfiguration
    {
        public IPEndPoint BrokerAdminEndPoint
        {
            get
            {
                if (BrokerAdminHost.IsNullOrEmpty())
                {
                    return new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerAdminPort);
                }
                else
                {
                    return SocketUtils.GetIPEndPointFromHostName(BrokerAdminHost, BrokerAdminPort);
                }
            }
        }

        public string BrokerAdminHost { get; set; }

        public int BrokerAdminPort { get; set; } = 10003;

        public IPEndPoint BrokerConsumerEndPoint
        {
            get
            {
                if (BrokerConsumerHost.IsNullOrEmpty())
                {
                    return new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerConsumerPort);
                }
                else
                {
                    return SocketUtils.GetIPEndPointFromHostName(BrokerConsumerHost, BrokerConsumerPort);
                }
            }
        }

        public string BrokerConsumerHost { get; set; }

        public int BrokerConsumerPort { get; set; } = 10002;

        public string BrokerGroupName { get; set; } = "DefaultGroup"


        public string BrokerName { get; set; } = "Default";

        public IPEndPoint BrokerProducerEndPoint
        {
            get
            {
                if (BrokerProducerHost.IsNullOrEmpty())
                {
                    return new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerProducerPort);
                }
                else
                {
                    return SocketUtils.GetIPEndPointFromHostName(BrokerProducerHost, BrokerProducerPort);
                }
            }
        }

        public string BrokerProducerHost { get; set; }

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
                        var endpoint = SocketUtils.GetIPEndPointFromHostName(array[0], int.Parse(array[1]));
                        ipEndPoints.Add(endpoint);
                    }
                }
                return ipEndPoints;
            }
        }

        public int NameServerPort { get; set; } = 10000;
    }
}