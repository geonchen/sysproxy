using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;


namespace sysproxy.Utils
{
    public static class SocketUtil
    {
        public static int GetFreePort(int defaultPort)
        {
            try
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                List<int> usedPorts = new List<int>();
                foreach (IPEndPoint endPoint in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners())
                {
                    usedPorts.Add(endPoint.Port);
                }
                for (int port = defaultPort; port <= 65535; port++)
                {
                    if (!usedPorts.Contains(port))
                    {
                        return port;
                    }
                }
            }
            catch
            {
                // in case access denied
                return defaultPort;
            }
            throw new Exception(I18N.GetString("No free port found"));
        }
    }
}
