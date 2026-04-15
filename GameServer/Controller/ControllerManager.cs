using GameServer.Servers;
using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace GameServer.Controller
{
    internal class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllers =
            new Dictionary<RequestCode, BaseController>();

        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;

            UserController userController = new UserController();
            controllers.Add(userController.GetRequestCode, userController);
        }

        public void HandleRequest(MainPack pack,Client client) {
            Console.WriteLine("进入handleRequest");
            //反射获取RequestCode所对应的controller，并写相应处理方法
            if (controllers.TryGetValue(pack.RequestCode, out BaseController controller))
            {
                string methodName = pack.ActionCode.ToString();
                //通过反射获取对应的处理函数
                MethodInfo methodInfo = controller.GetType().GetMethod(methodName);
                if (methodInfo == null) {
                    Console.WriteLine("Controllerz中没有对应的处理方法");
                }
                //反射获取方法的参数列表
                object[] parameters = new object[] { server, client , pack};
                object ret = methodInfo.Invoke(controller, parameters);
                if (ret!=null) {
                    client.Send(ret as MainPack);
                }
            }
            else {
                Console.WriteLine("没有找到对应的Controller处理");
            }
        }
    }
}
