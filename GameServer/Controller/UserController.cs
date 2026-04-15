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
            if (server.Logon(client, pack))
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
        
        
        }
    }
}
