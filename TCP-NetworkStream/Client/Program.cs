using System.Net;
using System.Net.Sockets;

//Thứ tự công việc bên phía Client
//1: Thiết lập IP và Port
//2: Kết nối tới Server
//4: Gửi và nhận dữ liệu phản hồi từ Server thông qua NetworkStream
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Client - Network Stream";

            //Nhập IP và Port
            // Ở đây mặc định là 127.0.0.1 và 1308
            Console.WriteLine("Server IP Address: ");
            var _serverIP = Console.ReadLine();
            var serverIP = IPAddress.Parse(_serverIP);

            Console.WriteLine("Server Port: ");
            var _serverPort = Console.ReadLine();
            var serverPort = int.Parse(_serverPort);

            TcpClient client = new TcpClient();
            client.Connect(serverIP, serverPort);
            Stream stream = client.GetStream();

            Console.WriteLine("Da ket noi toi Server");
            while (true)
            {
                //Dùng hàm StreamReader để nhận dữ liệu
                var reader = new StreamReader(stream);
                //Dùng hàm StreamWriter để gửi dữ liệu
                var writer = new StreamWriter(stream);

                //Gửi dữ liệu tới Server
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #Text >>> ");
                Console.ResetColor();
                string str = Console.ReadLine();

                writer.AutoFlush = true;
                writer.WriteLine(str);

                //Nhận phản hồi từ Server         
                str = reader.ReadLine();
                Console.WriteLine("Phan hoi tu Server: " + str);

            }
            stream.Close();
            client.Close();

            Console.Read();
        }
    }
}