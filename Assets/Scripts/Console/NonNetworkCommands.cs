using UnityEngine;
using QFSW.QC;
using RegularClasses.UDP;

namespace Console
{
    public class NonNetworkCommands : MonoBehaviour
    {
        [Command("u_ping")]
        public static void Ping(string ip)
        {
            CustomUdpClient.GetInstance().PingIp(ip);
        }
        
        [Command("u_message")]
        public static void SendUdpMessage(string message)
        {
            CustomUdpMessage.GetInstance().SendUdpMessage(message);
        }
    }
}