using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Client";
            
            Console.Write("Server IP address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.Write("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            var serverEndpoint = new IPEndPoint(serverIP, serverPort);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #Text >>> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                var client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                client.Connect(serverEndpoint);

                var stream = new NetworkStream(client);

                var sendBuffer = Encoding.UTF8.GetBytes(text+"\r\n");
                stream.Write(sendBuffer, 0, sendBuffer.Length);
                stream.Flush();

                var receiveBuffer = new byte[1024];
                var count = stream.Read(receiveBuffer, 0, 1024);
                var response = Encoding.UTF8.GetString(receiveBuffer, 0, count);
                Console.WriteLine(response.Trim());

                client.Close();
            }

        }
    }
}
