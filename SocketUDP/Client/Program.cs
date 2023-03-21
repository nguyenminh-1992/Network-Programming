using System.Text;
using System.Net;
using System.Net.Sockets;


//Thứ tự công việc bên phía Client
//1. Thiết lập IPEndpointvà Socket
//2. Kết nối Socket với IP Endpoint
//3. Gửi nhận dữ liệu từ Client
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "UDP CLient";

            //Nhập địa chỉ IPAddres và Port
            Console.Write("Server IP address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.Write("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            //Thiết lập IPEndpoint
            var serverEndpoint = new IPEndPoint(serverIP, serverPort);
            Console.Write("Type the text sent to the server \n");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
            //Kết nối socket với IPEndpoint
                var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                socket.Connect(serverEndpoint);

            // Gửi dữ liệu tới Server
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #Text >>> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                var sendBuffer = Encoding.ASCII.GetBytes(text);
                socket.SendTo(sendBuffer,serverEndpoint);
                EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0); // Lưu lại địa chỉ tiến trình nguồn, ở đây cụ thể là Server


            //Nhận dữ liệu từ Server
                var length = socket.ReceiveFrom(receiveBuffer,ref dummyEndpoint);
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Array.Clear(receiveBuffer, 0, size);

                Console.WriteLine($"Respond from Server <<< {result}");
                Console.WriteLine("-----------------------------------------");
            }
        }
    }
}

