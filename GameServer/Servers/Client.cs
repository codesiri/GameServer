using GameServer.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
namespace GameServer.Servers
{
   
    internal class Client
    {
        //客户端自己的socket
       private Socket socket;
        //每个clientt都需要一个缓冲区来接收数据(Message是封装后的缓冲区)
       private Message message;

        public Client(Socket socket) {
            this.socket = socket;
            message = new Message();
            StartReceive();
        }

        //服务器端接收数据的函数
        void StartReceive() {
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
            try
            {
                if (socket == null || socket.Connected) 
                {
                    return;
                }
                //服务器收到了多少数据
                int len = socket.EndReceive(iar);
                if (len == 0)
                {
                    //len为0说明客户端断开了连接
                    return;
                }
                message.ReadBuffer(len);
                StartReceive();
            }
            catch { 
            
            }

        }

    }
}
