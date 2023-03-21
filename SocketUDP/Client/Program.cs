using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "UDP CLient";
            Console.Write("Server IP address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.Write("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            var serverEndpoint = new IPEndPoint(serverIP, serverPort);
            Console.Write("Type the text sent to the server \n");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                // Gui di
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #Text >>> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                var socket = new Socket(SocketType.Dgram,ProtocolType.Udp);
                socket.Connect(serverEndpoint);

                var sendBuffer = Encoding.ASCII.GetBytes(text);
                socket.SendTo(sendBuffer,serverEndpoint);
                EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);


                //Nhan ve
                var length = socket.ReceiveFrom(receiveBuffer,ref dummyEndpoint);
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Array.Clear(receiveBuffer, 0, size);

                Console.WriteLine($"Respond from Server <<< {result}");
                Console.WriteLine("-----------------------------------------");
            }
        }
    }
}

