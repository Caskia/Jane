using Jane.Extensions;
using Jane.Utils;
using System;
using System.Collections.Generic;
using System.Net;

namespace Jane.ENode
{
    public class EQueueConfiguration : IEQueueConfiguration
    {
        private IPEndPoint _brokerAdminEndPoint;
        private IPEndPoint _brokerConsumerEndPoint;
        private IPEndPoint _brokerProducerEndPoint;
        private List<IPEndPoint> _nameServerEndPoints;

        public IPEndPoint BrokerAdminEndPoint
        {
            get
            {
                if (_brokerAdminEndPoint != null)
                {
                    return _brokerAdminEndPoint;
                }

                if (BrokerAdminHost.IsNullOrEmpty())
                {
                    _brokerAdminEndPoint = new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerAdminPort);
                }
                else
                {
                    _brokerAdminEndPoint = SocketUtils.GetIPEndPointFromHostName(BrokerAdminHost, BrokerAdminPort);
                }

                return _brokerAdminEndPoint;
            }
        }

        public string BrokerAdminHost { get; set; }

        public int BrokerAdminPort { get; set; } = 10003;

        public IPEndPoint BrokerConsumerEndPoint
        {
            get
            {
                if (_brokerConsumerEndPoint != null)
                {
                    return _brokerConsumerEndPoint;
                }

                if (BrokerConsumerHost.IsNullOrEmpty())
                {
                    _brokerConsumerEndPoint = new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerConsumerPort);
                }
                else
                {
                    _brokerConsumerEndPoint = SocketUtils.GetIPEndPointFromHostName(BrokerConsumerHost, BrokerConsumerPort);
                }

                return _brokerConsumerEndPoint;
            }
        }

        public string BrokerConsumerHost { get; set; }

        public int BrokerConsumerPort { get; set; } = 10002;

        public string BrokerGroupName { get; set; } = "DefaultGroup";

        public string BrokerName { get; set; } = "Default";

        public IPEndPoint BrokerProducerEndPoint
        {
            get
            {
                if (_brokerProducerEndPoint != null)
                {
                    return _brokerProducerEndPoint;
                }

                if (BrokerProducerHost.IsNullOrEmpty())
                {
                    _brokerProducerEndPoint = new IPEndPoint(SocketUtils.GetLocalIPV4(), BrokerProducerPort);
                }
                else
                {
                    _brokerProducerEndPoint = SocketUtils.GetIPEndPointFromHostName(BrokerProducerHost, BrokerProducerPort);
                }

                return _brokerProducerEndPoint;
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
                if (_nameServerEndPoints != null && _nameServerEndPoints.Count > 0)
                {
                    return _nameServerEndPoints;
                }

                _nameServerEndPoints = new List<IPEndPoint>();
                if (string.IsNullOrWhiteSpace(NameServerAddress))
                {
                    var defaultNameServer = new IPEndPoint(SocketUtils.GetLocalIPV4(), NameServerPort);
                    _nameServerEndPoints.Add(defaultNameServer);
                }
                else
                {
                    var addressList = NameServerAddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var address in addressList)
                    {
                        var array = address.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        var endpoint = SocketUtils.GetIPEndPointFromHostName(array[0], int.Parse(array[1]));
                        _nameServerEndPoints.Add(endpoint);
                    }
                }
                return _nameServerEndPoints;
            }
        }

        public int NameServerPort { get; set; } = 10000;
    }
}