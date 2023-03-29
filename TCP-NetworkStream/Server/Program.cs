using System.Net;
using System.Net.Sockets;


//Thứ tự công việc bên phía Server
//1: Thiết lập IP và Port
//2: Chấp nhận kết nối từ Client
//3: Kết nối NetworkStream
//4: Nhận và gửi dữ liệu tới Client thông qua NetworkStream
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Server - Network Stream";
            var localIP = IPAddress.Any;
            var localPort = 1308;
            TcpListener listener = new TcpListener(localIP, localPort);

            listener.Start();
            Console.WriteLine("Bat dau tai " + listener.LocalEndpoint);
            Console.WriteLine("Dang cho ket noi tu Client ...");

            Socket socket = listener.AcceptSocket();
            Console.WriteLine("Da nhan ket noi tu Client" + socket.RemoteEndPoint);

            var stream = new NetworkStream(socket);

            //Dùng hàm StreamReader để nhận dữ liệu
            var reader = new StreamReader(stream);
            //Dùng hàm StreamWriter để gửi dữ liệu
            var writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            while (true)
            {
                //Nhận dữ liệu từ Client
                string str = reader.ReadLine();
                Console.WriteLine("Da nhan du lieu tu Client: " + str);

                //Chuyển dữ liệu sang dạng chữ hoa và gửi lại
                string result = str.ToUpper();
                writer.WriteLine(result);

                Console.WriteLine("----------------");

            }
            stream.Close();
            socket.Close();
            listener.Stop();
        }


    }
}