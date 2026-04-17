using GameServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    internal class Program
    {
        public static void Main(string[] args) 
        {
            Server server = new Server(65531);
            Console.WriteLine("服务端已启动");
            Console.Read();
        }
    }
}
