using System.Net.Sockets;

namespace GameServer.Servers
{
    public class Server
    {
        private Socket socket;

        private List<Client> clients = new List<Client>();

        public Server(int port) {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
            socket.Listen(0);
        }

        void StartAccept() { 
            socket.BeginAccept(AcceptCallback, null);
        }

        void AcceptCallback(IAsyncResult ar) {
            Socket client = socket.EndAccept(ar);
            Console.WriteLine("Client connected: " + client.RemoteEndPoint.ToString());
            //将客户端加入到列表中
            clients.Add(new Client(client));
            StartAccept();
        }

    }
}
