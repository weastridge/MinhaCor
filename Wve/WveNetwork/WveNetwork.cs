using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace Wve
{
    /// <summary>
    /// tools for working with networks
    /// </summary>
    public class WveNetwork
    {
        /// <summary>
        /// This is adapted from a Code Project article called
        /// Detect Internet Network Availability By stevenmcohn.
        /// Evaluate the online network adapters to determine if at least one of them
        /// is capable of connecting to the Internet.
        /// (Note it still reports true if we're connected to a network that isn't 
        /// itself connected to the Internet)
        /// His project includes event handlers to keep updated a bool value to indicate
        /// connectivity for rapid response to caller.
        /// </summary>
        /// <returns></returns>

        public static bool IsNetworkAvailable()
        {
            // only recognizes changes related to Internet adapters
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // however, this will include all adapters
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface face in interfaces)
                {
                    // filter so we see only Internet adapters
                    if (face.OperationalStatus == OperationalStatus.Up)
                    {
                        if ((face.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                            (face.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                        {
                            IPv4InterfaceStatistics statistics = face.GetIPv4Statistics();

                            // all testing seems to prove that once an interface comes online
                            // it has already accrued statistics for both received and sent...

                            if ((statistics.BytesReceived > 0) &&
                                (statistics.BytesSent > 0))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}
