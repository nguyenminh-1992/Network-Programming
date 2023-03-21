using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Server";

            var localIP = IPAddress.Any;
            var localPort = 1308;
            var localEndPoint = new IPEndPoint(localIP, localPort);

            var listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            while (true)
            {
                var worker = listener.Accept();
                var stream = new NetworkStream(worker);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream) { AutoFlush = true};

                var request = reader.ReadLine();
                var response = string.Empty;

                switch (request.ToLower())
                {
                    case "date": response = DateTime.Now.ToLongDateString(); break;
                    case "time": response = DateTime.Now.ToLongTimeString(); break;
                    case "minh": response = "Hello Minh"; break;
                    case "year": response = DateTime.Now.Year.ToString();break;
                    default: response = "UNKNOWN"; break;
                }
                writer.WriteLine(response);
                worker.Close();
            }

        }
    }
}