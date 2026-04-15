using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.DAO
{
    internal class UserData
    {
        private MySqlConnection mysqlCon;

        private string connstr = "database=sya:data source=43.251.227.214;password=123456;pooling=false;charset=utf8;port=3306";
        public UserData() 
        {
        }
        private void ConnectMysql() 
        {
            try
            {
                this.mysqlCon = new MySqlConnection(connstr);
                this.mysqlCon.Open();
            }
            catch (Exception e)
            {   
                Console.WriteLine(e.Message);
                Console.WriteLine("链接数据库失败");
;            }        
        }

        public bool Logon(MainPack pack) 
        {
            string username = pack.LoginPack.Username;
            string password = pack.LoginPack.Password;
            string sql2 = "Select FORM `sys`.`userdata` WHERE username='@username'";
            MySqlCommand comd2 = new MySqlCommand(sql2,mysqlCon);
            MySqlDataReader read2 =  comd2.ExecuteReader();
            if (read2.Read()) {
                //用户名已经被注册
                return false;
            }
            string sql = "INSERT INTO `sys`.`userdata` (`username`,`password`) VALUES ('@username','@password')";
            MySqlCommand comd = new MySqlCommand(sql);
            comd = new MySqlCommand(sql, mysqlCon);
            try {
                comd.ExecuteNonQuery();
                return true;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
            
        } 
    }
}
