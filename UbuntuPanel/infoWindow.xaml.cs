using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            TimeStart(0,0,2);
            
        }

        private void TimeStart(int hours, int minutes, int seconds)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(hours, minutes, seconds);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var client = new SshClient(Server.ServerIP, Server.ServerPort, Server.ServerUsername, Server.ServerPassword);
                try
                {
                    client.Connect();
                }
                catch (Exception)
                {
                    Dispatcher.Invoke(() =>
                    {
                        theViewbox.Items.Add("No connection...");
                    });
                }
                var shell = client.CreateShellStream("Tail", 80, 24, 800, 600, 1024);

                StreamWriter wr = new StreamWriter(shell);
                StreamReader rd = new StreamReader(shell);
                wr.AutoFlush = true;
                wr.WriteLine("top");
                string rep = shell.Read();
            });
        }
    }
}
