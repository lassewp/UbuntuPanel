using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UbuntuPanel
{
    /// <summary>
    /// Interaction logic for infoWindow.xaml
    /// </summary>
    public partial class infoWindow : Window
    {
        Server Server;
        public infoWindow(Server server)
        {
            InitializeComponent();
            Server = server;
            serverNameLabel.Content = Server.ServerName;

            Task.Run(() =>
            {
                SshClient sshClient;
                if (Server.ServerPort != 0)
                {
                    sshClient = new SshClient(Server.ServerIP, Server.ServerPort, Server.ServerUsername, Server.ServerPassword);
                }
                else
                {
                    sshClient = new SshClient(Server.ServerIP, Server.ServerUsername, Server.ServerPassword);
                }


                sshClient.Connect();
                while (sshClient.IsConnected)
                {

                    var processTxt = sshClient.RunCommand("top -b -n1").Result.Remove(0, 6);


                    Dispatcher.Invoke(() => 
                    { 
                        ProcessBox.Text = processTxt; 
                        ProcessBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    });
                }




            });

            Task.Run(() =>
            {
                SshClient sshClient;
                if (Server.ServerPort != 0)
                {
                    sshClient = new SshClient(Server.ServerIP, Server.ServerPort, Server.ServerUsername, Server.ServerPassword);
                }
                else
                {
                    sshClient = new SshClient(Server.ServerIP, Server.ServerUsername, Server.ServerPassword);
                }


                sshClient.Connect();
                while (sshClient.IsConnected)
                {
                    var uptimeTxt = sshClient.RunCommand("uptime -p").Result.TrimStart().TrimEnd();
                    var spaceAvailable = sshClient.RunCommand("df | awk '{SUM+=$4}END{print SUM}'").Result.TrimStart().TrimEnd();
                    var spaceTotal = sshClient.RunCommand("df | awk '{SUM+=$2}END{print SUM}'").Result.TrimStart().TrimEnd();
                    var ramTotal = sshClient.RunCommand("free -m | grep Mem | awk '{print$2}'").Result.TrimStart().TrimEnd();
                    var ramUsed = sshClient.RunCommand("free -m | grep Mem | awk '{print $4/$2 * 100.0}'").Result.TrimStart().TrimEnd();
                    var cpuTxt = sshClient.RunCommand("top -bn2 | grep '%Cpu' | tail -1 | grep -P '(....|...) id,'|awk '{print \"\" 100-$8 \" % \"}'").Result.TrimStart().TrimEnd();
                    

                    Dispatcher.Invoke(() =>
                    {
                        uptimeLabel.Content = uptimeTxt;
                        cpuLabel.Content = cpuTxt;
                        ramLabel.Content = "Total: " + ramTotal + "Mb " + "-" + " Used: " + ramUsed + "%";
                        diskLabel.Content = "Available: " + Convert.ToInt32(spaceAvailable)/1024 + "MB" + " - " + "Total: " + Convert.ToInt32(spaceTotal) / 1024 + "MB";
                    });
                }




            });

        }
    }
}
