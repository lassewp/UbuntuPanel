using System;
using System.Collections.Generic;
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
    /// Interaction logic for ControlPanelWindow.xaml
    /// </summary>
    public partial class ControlPanelWindow : Window
    {
        List<Server> servers = new List<Server>();

        public ControlPanelWindow()
        {
            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {

            });
        }

        private void serverOneEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();
                editWindow.ShowDialog();
                if (editWindow.IsSaved)
                {
                    string tmpName = editWindow.ServerNameTextBox.Text;
                    string tmpIP = editWindow.ServerTextBox.Text;
                    string tmpPort = editWindow.PortTextBox.Text;
                    string tmpPass = editWindow.PasswordTextBox.Text;
                    string tmpUser = editWindow.UserTextBox.Text;

                    Dispatcher.Invoke(() =>
                    {
                        var newServer = new Server(tmpName,tmpIP,Convert.ToInt32(tmpPort), tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(0) == null)
                            servers.Add(newServer);
                        else
                            servers[0] = newServer;

                        serverOneName.Content = servers[0].ServerName;
                        if (servers[0].ServerPort.ToString() != "")
                            serverOneIP.Content = servers[0].ServerIP + ":" + servers[0].ServerPort;
                        else
                            serverOneIP.Content = servers[0].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }
    }
}
