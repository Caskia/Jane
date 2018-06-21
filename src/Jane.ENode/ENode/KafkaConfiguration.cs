using Jane.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Jane.ENode
{
    public class KafkaConfiguration : IKafkaConfiguration
    {
        private List<IPEndPoint> _brokerEndPoints;

        public string BrokerAddresses { get; set; }

        public List<IPEndPoint> BrokerEndPoints
        {
            get
            {
                if (_brokerEndPoints != null && _brokerEndPoints.Count > 0)
                {
                    return _brokerEndPoints;
                }

                _brokerEndPoints = new List<IPEndPoint>();
                if (string.IsNullOrWhiteSpace(BrokerAddresses))
                {
                    var defaultNameServer = new IPEndPoint(SocketUtils.GetLocalIPV4(), 9092);
                    _brokerEndPoints.Add(defaultNameServer);
                }
                else
                {
                    var addressList = BrokerAddresses.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var address in addressList)
                    {
                        var array = address.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        var hostNameType = Uri.CheckHostName(array[0]);
                        IPEndPoint ipEndPoint;
                        switch (hostNameType)
                        {
                            case UriHostNameType.Dns:
                                ipEndPoint = SocketUtils.GetIPEndPointFromHostName(array[0], int.Parse(array[1]), AddressFamily.InterNetwork, false);
                                break;

                            case UriHostNameType.IPv4:
                            case UriHostNameType.IPv6:
                                ipEndPoint = new IPEndPoint(IPAddress.Parse(array[0]), int.Parse(array[1]));
                                break;

                            case UriHostNameType.Unknown:
                            default:
                                throw new Exception($"Host name type[{hostNameType}] can not resolve.");
                        }
                        _brokerEndPoints.Add(ipEndPoint);
                    }
                }
                return _brokerEndPoints;
            }
        }
    }
}