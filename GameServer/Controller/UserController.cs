using GameServer.Servers;
using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controller
{
    class UserController:BaseController
    {
        public UserController() {
            requestCode = SocketGameProtocol.RequestCode.User;
        }

        /// <summary>
        /// 注册请求
        /// </summary>
        /// <returns></returns>
        public MainPack Logon(Server server, Client client, MainPack pack) {
            //处理登录逻辑，委托给了server.Logon函数
            Console.WriteLine("UserController处理.......");
            Console.WriteLine($"解析到的数据包信息：{pack.LoginPack.Username} ${pack.LoginPack.Password}");
            if (client.Logon(pack))
            {
                pack.ReturnCode = ReturnCode.Success;
            }
            else {
                pack.ReturnCode = ReturnCode.Fail;
            }
            return pack;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public MainPack LogIn(Server server, Client client, MainPack pack) {
            Console.WriteLine("用户登录请求");
            return pack;
        }
    }
}
