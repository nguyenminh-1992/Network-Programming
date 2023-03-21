using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Server";

            var localIP = IPAddress.Any;
            var localPort = 1308;
            var localEndPoint = new IPEndPoint(localIP, localPort);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                var length = socket.ReceiveFrom(receiveBuffer,ref remoteEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Received from {remoteEndpoint}: {text}");

                var result = text.ToUpper();
                var sendBuffer = Encoding.ASCII.GetBytes(result);

                socket.SendTo(sendBuffer,remoteEndpoint);
                Console.WriteLine($"Send to Client: {result}");

                Console.WriteLine("-----------------------------------------");

                Array.Clear(receiveBuffer, 0, size);
            }

        }
    }
}