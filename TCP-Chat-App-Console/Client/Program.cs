﻿using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] data = new byte[1024];
            string input, stringData;
            IPEndPoint IPendpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(IPendpoint);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }
            int recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
            Console.WriteLine(stringData);
            while (true)
            {
                Console.Write("You: ");
                input = Console.ReadLine();
                if (input == "exit")
                    break;
                //Console.WriteLine("You: " + input);
                server.Send(Encoding.UTF8.GetBytes(input));
                data = new byte[1024];
                recv = server.Receive(data);
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                byte[] utf8string = Encoding.UTF8.GetBytes(stringData);
                Console.WriteLine("Server: " + stringData);
            }
            Console.WriteLine("Disconnecting from server...");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Console.WriteLine("Disconnected!");
            Console.ReadLine();

        }
    }
}