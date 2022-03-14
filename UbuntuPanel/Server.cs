using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbuntuPanel
{
    public class Server
    {
        public string ServerName;
        public string ServerIP;
        public int ServerPort;
        public string ServerUsername;
        public string ServerPassword;

        public Server(string serverName, string serverIP, int serverPort, string username, string password)
        {
            this.ServerName = serverName;
            this.ServerIP = serverIP;
            this.ServerPort = serverPort;
            this.ServerUsername = username;
            this.ServerPassword = password;
        }

    }
}
