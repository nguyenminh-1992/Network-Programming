using System.Text;
using System.Net;
using System.Net.Sockets;

//Thứ tự công việc bên phía Client
//1. Thiết lập IPEndpointvà Socket
//2. Kết nối Socket với IP Endpoint
//3. Gửi nhận dữ liệu từ Server

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP CLient";

            //Nhập địa chỉ IPAddress và Port
            Console.Write("Server IP address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.Write("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            //Thiết lập IPEndPoint
            var serverEndpoint = new IPEndPoint(serverIP, serverPort);
            Console.Write("Type the text sent to the server \n");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
            //Kết nối Socket với IPEndpoint
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(serverEndpoint);

            //Gửi dữ liệu tới Server
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #Text >>> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                var sendBuffer = Encoding.ASCII.GetBytes(text);
                socket.Send(sendBuffer);
                socket.Shutdown(SocketShutdown.Send); //Đóng kết nối, không gửi dữ liệu nữa 


            //Nhận dữ liệu từ Server
                var length = socket.Receive(receiveBuffer);
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Respond from Server <<< {result}");
                Console.WriteLine("-----------------------------------------");
                socket.Shutdown(SocketShutdown.Receive); //Đóng kết nối, không nhận dữ liệu nữa

                socket.Close();

            //Xóa bộ đệm
                Array.Clear(receiveBuffer, 0, size);
            }
        }
    }
}

