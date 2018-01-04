using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Jane.Utils
{
    public class SocketUtils
    {
        public static IPAddress GetLocalIPV4()
        {
            var networkTypes = new List<NetworkInterfaceType>()
            {
                NetworkInterfaceType.Ethernet,
                NetworkInterfaceType.Wireless80211
            };
            var hasGatewayNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.GetIPProperties().GatewayAddresses.Count > 0);
            var ethernetNetworkInterfaces = hasGatewayNetworkInterfaces
                .Where(ni => networkTypes.Contains(ni.NetworkInterfaceType));

            return ethernetNetworkInterfaces
                .Select(ni => ni.GetIPProperties().UnicastAddresses.First(x => x.Address.AddressFamily == AddressFamily.InterNetwork).Address)
                .First();
        }
    }
}