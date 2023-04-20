using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int recv;
            IPEndPoint IPendpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IPendpoint);
            socket.Listen(10);
            Console.WriteLine("Waiting for a client...");
            Socket client = socket.Accept();
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
            string welcome = "Welcome to my test server";
            data = Encoding.UTF8.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);
            string input;
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                    break;
                Console.WriteLine("Client: " + Encoding.UTF8.GetString(data, 0, recv));
                Console.Write("You: ");
                input = Console.ReadLine();
                client.Send(Encoding.UTF8.GetBytes(input));
            }
            
            Console.WriteLine("Disconnected from {0}", clientep.Address);
            client.Close();
            socket.Close();
            Console.ReadLine();

        }
    }
}