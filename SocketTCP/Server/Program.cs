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

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                var socket = listener.Accept();
                Console.WriteLine($"Accepted connection from {socket.RemoteEndPoint}");

                var length = socket.Receive( receiveBuffer );
                socket.Shutdown(SocketShutdown.Receive);
                var text = Encoding.ASCII.GetString( receiveBuffer ,0,length);
                Console.WriteLine($"Received from Client: {text}");

                var result = text.ToUpper();    

                var sendBuffer = Encoding.ASCII.GetBytes(result);

                socket.Send(sendBuffer);
                Console.WriteLine($"Send to Client: {result}");

                socket.Shutdown(SocketShutdown.Send);

                Console.WriteLine($"Closing connection from {socket.RemoteEndPoint}\n\n");
                Console.WriteLine("-----------------------------------------");
                socket.Close();

                Array.Clear(receiveBuffer, 0, size);

            }

        }
    }
}