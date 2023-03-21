using System.Text; // Để sử dụng lớp Encoding
using System.Net; // Để sử dụng lớp IPAddress, IPEndPoint
using System.Net.Sockets; // Để sử dụng lớp Socket

//Thứ tự công việc bên phía Sever:
//1. Thiết lập IPEndpoint và Socket
//2. Kết nối Socket với IP Endpoint
//3. Lắng nghe từ Client
//4. Chấp nhận kết nối từ Client
//5. Gửi nhận dữ liệu từ Client

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
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//Gửi dữ liệu theo giao thức TCP, loại Socket Stream, họ địa chỉ IPv4
            listener.Bind(localEndPoint); //Kết nối Socket và EndPoint
            listener.Listen(10); //Lắng nghe từ Client, tối đa 10 request mỗi lần
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
            // Chấp nhận kết nối từ Client
                var socket = listener.Accept();
                Console.WriteLine($"Accepted connection from {socket.RemoteEndPoint}");

            //Nhận dữ liệu từ Client
                var length = socket.Receive( receiveBuffer );
                socket.Shutdown(SocketShutdown.Receive);
                var text = Encoding.ASCII.GetString( receiveBuffer ,0,length);
                Console.WriteLine($"Received from Client: {text}");
                socket.Shutdown(SocketShutdown.Receive); //Đóng kết nối, không nhận dữ liệu nữa

            //Chuyển dữ liệu sang chữ in hoa và gửi lại Client
                var result = text.ToUpper();    
                var sendBuffer = Encoding.ASCII.GetBytes(result);
                socket.Send(sendBuffer);
                Console.WriteLine($"Send to Client: {result}");
                socket.Shutdown(SocketShutdown.Send); //Đóng kết nối, không gửi dữ liệu nữa

                Console.WriteLine($"Closing connection from {socket.RemoteEndPoint}\n\n");
                Console.WriteLine("-----------------------------------------");
                socket.Close();

            //Xóa bộ đệm
                Array.Clear(receiveBuffer, 0, size);

            }

        }
    }
}