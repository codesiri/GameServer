using GameServer.Controller;
using GameServer.DAO;
using SocketGameProtocol;
using System.Net.Sockets;

namespace GameServer.Servers
{
    class Server
    {
        private Socket socket;

        private List<Client> clients = new List<Client>();

        private ControllerManager controllerManager;

        private UserData userData;

        public ControllerManager ControllerManager { get { return controllerManager; } }

        public Server(int port) {
            this.controllerManager = new ControllerManager(this);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
            socket.Listen(0);
            StartAccept();
        }

        void StartAccept() { 
            socket.BeginAccept(AcceptCallback, null);
        }

        void AcceptCallback(IAsyncResult ar) {
            Socket client = socket.EndAccept(ar);
            Console.WriteLine("Client connected: " + client.RemoteEndPoint.ToString());
            //将客户端加入到列表中
            clients.Add(new Client(client,this));
            StartAccept();
        }



        public void HandleRequest(MainPack pack,Client client) 
        {
            controllerManager.HandleRequest(pack, client);
        }
    }
}
