using GameServer.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using GameServer.DAO;
using SocketGameProtocol;
namespace GameServer.Servers
{
   
     class Client
    {
        //客户端自己的socket
        private Socket socket;
        //每个clientt都需要一个缓冲区来接收数据(Message是封装后的缓冲区)
        private Message message;
        private  UserData userData;
        private Server server;
        public UserData GetUserData
        {
            get { return userData; }
        }
        public Client(Socket socket,Server server) {
            this.userData = new UserData();
            message = new Message();
            this.server = server;
            this.socket = socket;
            StartReceive();
        }

        //服务器端接收数据的函数
        void StartReceive() {
            Console.WriteLine("继续接受消息");
            socket
                .BeginReceive(
                message.Buffer,
                message.StartIndex,
                message.Remsize, 
                SocketFlags.None, 
                ReceiveCallBack,
                null);
        }

        void ReceiveCallBack(IAsyncResult iar) {
            //客户端给服务器发消息，服务器的接受函数
            Console.WriteLine("收到消息");
            try
            {
                if (socket == null || !socket.Connected) 
                {
                    return;
                }
                //服务器收到了多少数据
                int len = socket.EndReceive(iar);
                Console.WriteLine(len);
                if (len == 0)
                {
                    //len为0说明客户端断开了连接
                    return;
                }
                //读取解析数据包
                message.ReadBuffer(len, HandleRequest);
                Console.WriteLine("============================================");
                StartReceive();
            }
            catch { 
            
            }

        }
        //给客户端发包的函数
        public void Send(MainPack pack)
        {
            socket.Send(Message.PackData(pack));
        }

        void HandleRequest(MainPack pack) {
            this.server.ControllerManager.HandleRequest(pack,this);
        }

        public bool Logon( MainPack pack)
        {
            return true;
            //return this.GetUserData.Logon(pack);
        }
    }
}
