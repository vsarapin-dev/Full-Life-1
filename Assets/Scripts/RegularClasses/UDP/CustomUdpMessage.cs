using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RegularClasses.UDP
{
    public class CustomUdpMessage
    {
        private CustomUdpMessage() { }
        
        private static CustomUdpMessage _instance;
        
        public static CustomUdpMessage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomUdpMessage();
            }
            return _instance;
        }
        
        public void SendUdpMessage(string message)
        {
            UdpClient udpClient = new UdpClient(7777);
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 7777);
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            udpClient.Send(sendBytes, sendBytes.Length, remoteEndPoint);
        }
    }
}