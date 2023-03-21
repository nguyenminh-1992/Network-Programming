using System.Text; // Để sử dụng lớp Encoding
using System.Net; // Để sử dụng lớp IPAddress, IPEndPoint
using System.Net.Sockets; // Để sử dụng lớp Socket

//Thứ tự công việc bên phía Sever:
//1. Thiết lập IPEndpoint và Socket
//2. Kết nối Socket với IP Endpoint
//3. Gửi nhận dữ liệu từ Client

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Server";

            //Thiết lập IPEndpoint
            var localIP = IPAddress.Any; //Chấp nhận tất cả địa chỉ IPAddress
            var localPort = 1308; // Nghe tất cả các gói tin gửi qua cổng 1308
            var localEndPoint = new IPEndPoint(localIP, localPort);

            //Thiết lập Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);// Gửi dữ liệu theo giao thức UDP, loại socket Dgram, Internetwork: họ địa chỉ IPv4
            socket.Bind(localEndPoint); // Kết nối Socket và EndPoint
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
            // Nhận dữ liệu từ Client
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0); // Chứa địa chỉ của Client
                var length = socket.ReceiveFrom(receiveBuffer,ref remoteEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Received from {remoteEndpoint}: {text}");

            // Chuyển dữ liệu sang chữ In hoc và gửi lại Client
                var result = text.ToUpper();
                var sendBuffer = Encoding.ASCII.GetBytes(result);
                socket.SendTo(sendBuffer,remoteEndpoint);
                Console.WriteLine($"Send to Client: {result}");

                Console.WriteLine("-----------------------------------------");

            //Xóa bộ đệm
                Array.Clear(receiveBuffer, 0, size);
            }

        }
    }
}