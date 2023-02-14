using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace server
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Console Chat");

            try
            {
                Console.WriteLine("This is Server! ");

                IPHostEntry host = Dns.GetHostEntry("localhost");
                /// IPAddress ip = IPAddress.Parse("");
                IPAddress ip = host.AddressList[0];

                IPEndPoint ipe = new IPEndPoint(ip, 12000);

                Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(ipe);

                listener.Listen(10);

                Socket handler = listener.Accept();

                string data = "";

                while (true)
                {
                    byte[] buffer = new byte[1024];
                    handler.Receive(buffer);

                    data = Encoding.ASCII.GetString(buffer);

                    Console.WriteLine("Client: " + data.Replace("\0", "").TrimEnd().TrimStart());

                    byte[] buffer2 = new byte[1024];
                    data = Console.ReadLine();

                    buffer2 = Encoding.ASCII.GetBytes(data);
                    handler.Send(buffer2);
                    if (data.Replace("\0", "").TrimEnd().TrimStart().Equals("Bye", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }


                }

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
