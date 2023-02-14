using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Tcp_Clients_chat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("This is client");
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ip = host.AddressList[0];
                IPEndPoint remote = new IPEndPoint(ip, 12000);

                Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remote);


                byte[] buffer = new byte[1024];
                while (true)
                {

                    string data = Console.ReadLine();

                    buffer = Encoding.UTF8.GetBytes(data); 

                    sender.Send(buffer);
                    byte[] buffer2 = new byte[1024];

                    sender.Receive(buffer2);

                    data = Encoding.UTF8.GetString(buffer2);

                    Console.WriteLine("server : " + data.Replace("\0", "").TrimEnd().TrimStart());
                    if (data.Replace("\0", "").TrimEnd().TrimStart().Equals("Bye", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
                sender.Shutdown(SocketShutdown.Both);

                sender.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
