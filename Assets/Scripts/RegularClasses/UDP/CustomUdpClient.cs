using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using System.Net.Sockets;

namespace RegularClasses.UDP
{
    public class CustomUdpClient
    {
        private UdpClient _client;
        private Regex _regexParse;
        private Thread _threadUDP;
        private IPEndPoint _remoteIpEndPoint;
        
        private CustomUdpClient() { }
        
        private static CustomUdpClient _instance;
        
        public static CustomUdpClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomUdpClient();
            }
            return _instance;
        }
        
        public void PingIp(string ip)
        {
            string ipAddress = "";
            int port = 80;

            string[] addressComponents = ip.Split(':');
            if (addressComponents.Length == 2)
            {
                ipAddress = addressComponents[0];
                int.TryParse(addressComponents[1], out port);
            }

            //StartPing(ipAddress, port);
            StartPing();
        }

        //private void StartPing(string ip, int port)
        private void StartPing()
        {
            _client = new UdpClient(7777);
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            _remoteIpEndPoint = new IPEndPoint(ipAddress, 7777);
            _regexParse = new Regex(@"\d*$");
            _threadUDP = new Thread(UdpRead);
            _threadUDP.Name = "Mindtuner UDP thread";
            _threadUDP.Start();
        }

        public void UdpRead()
        {
            while (true)
            {
                try
                {
                    Debug.Log("listening UDP port " + 7777);
                    var receiveBytes = _client.Receive(ref _remoteIpEndPoint);
                    var returnData = Encoding.ASCII.GetString(receiveBytes);

                    Debug.Log(returnData);
                    Debug.Log(_regexParse.Match(returnData).ToString());
                }
                catch (Exception e)
                {
                    Debug.Log("Not so good " + e);
                }

                Thread.Sleep(20);
            }
        }

        

        private void CloseUdp()
        {
            _threadUDP?.Abort();
            _client.Close();
        }
    }
}