using System;
using System.Net;
using System.Net.Sockets;

namespace OSC.NET
{
    /// <summary>
    /// OSCReceiver
    /// </summary>
    public class OSCReceiver
    {
        protected UdpClient udpClient;
        protected int localPort;

        public OSCReceiver(int localPort)
        {
            this.localPort = localPort;
            Connect();
        }

        public void Connect()
        {
            if (udpClient != null)
            {
                Close();
            }

            udpClient = new UdpClient(localPort);
        }

        public void Close()
        {
            udpClient?.Close();
            udpClient = null;
        }

        public OSCPacket Receive()
        {
            try
            {
                IPEndPoint ip = null;
                byte[] bytes = udpClient.Receive(ref ip);

                if (bytes != null && bytes.Length > 0)
                {
                    return OSCPacket.Unpack(bytes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return null;
        }
    }
}