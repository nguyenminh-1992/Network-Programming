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

            //Thiết lập IPEndpoint
            var localIP = IPAddress.Any; //Chấp nhận tất cả địa chỉ IPAddress
            var localPort = 1308; // Nghe tất cả các gói tin gửi qua cổng 1308
            var localEndPoint = new IPEndPoint(localIP, localPort);

            var listener = new Socket(SocketType.Stream, ProtocolType.Tcp); //Gửi dữ liệu theo giao thức TCP, loại Socket Stream
            listener.Bind(localEndPoint); //Kết nối Socket và EndPoint
            listener.Listen(10); //Lắng nghe từ Client, tối đa 10 request mỗi lần
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waitting for request ....");

            while (true)
            {
            //Chấp nhận kết nối từ Client
                var socket = listener.Accept();
                Console.WriteLine($"Accepted connection from {socket.RemoteEndPoint}");

                var stream = new NetworkStream(socket); //Nhận dữ liệu từ Client
                var reader = new StreamReader(stream);//Đọc chuỗi truy vấn
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
                socket.Close();
            }

        }
    }
}