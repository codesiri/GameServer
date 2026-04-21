using Google.Protobuf;
using SocketGameProtocol;

namespace GameServer.Tool
{


    //message的对象代表的是一个完整的数据包
    class Message
    {
        public static readonly int PACK_HEAD_LEN = 4;
        private byte[] buffer = new byte[1024];
        private int startindex;
        public byte[] Buffer { get { return buffer; } }

        public int StartIndex
        {
            get { return startindex; }
        }

        public int Remsize
        {
            get
            {
                return buffer.Length - startindex;
            }
        }

        public void ReadBuffer(int len, Action<MainPack> handleRquest)
        {
            Console.WriteLine("解析协议");
            startindex += len;
            while (true)
            {
                Console.WriteLine("解析消息循环");
                //因为包头是4字节的长度信息，所以如果startindex小于4，说明包头还没有接收完整，不能进行下一步的处理
                if (startindex <= PACK_HEAD_LEN)
                {
                    return;
                }
                //从buffer0开始往后读取四个字节
                int count = BitConverter.ToInt32(buffer, 0);
                //循环解析消息
                //如果startindex大于等于包头长度加上包体长度，说明这个包已经接收完整了，可以进行下一步的处理了
                if (startindex >= (count + PACK_HEAD_LEN))
                {
                    MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                    //处理解析过后的数据包
                    handleRquest(pack);
                    //这一段处理粘包，略过第一个包体，将剩下的字节往前移动
                    //，也就是第二个或者第三个包体的开始位置，startindex - count-4 是第二个包体没被处理的字节数
                    Array.Copy(buffer, count + 4, buffer, 0, startindex - count - 4);
                    startindex -= (count + 4);
                }
                else
                {
                    //如果startindex小于包头长度加上包体长度，说明这个包还没有接收完整，不能进行下一步的处理了
                    break;
                }
            }
        }
        public static byte[] PackData(MainPack pack)
        {
            byte[] data = pack.ToByteArray();
            byte[] head = BitConverter.GetBytes(data.Length);//包头
            //组装包头，包体
            return head.Concat(data).ToArray();
        }
    }
}
