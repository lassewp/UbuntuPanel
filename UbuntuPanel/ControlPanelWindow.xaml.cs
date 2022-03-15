using Renci.SshNet;
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
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Server Status checker:
            Task.Run(() =>
            {
                foreach (var server in servers)
                {
                    Task.Run(() =>
                    {
                        string ip = server.ServerIP;
                        string port = server.ServerPort.ToString();
                        string user = server.ServerUsername;
                        string pass = server.ServerPassword;
                        SshClient client;

                        if (port != "")
                        {
                            client = new SshClient(ip, Convert.ToInt32(port), user, pass);
                            bool connected = false;
                            try
                            {
                                client.Connect();
                                connected = true;
                            }
                            catch (Exception)
                            {
                                connected = false;
                            }

                            if (connected == true)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    serverOneStatus.Content = "Connected";
                                    serverOneIndicator.Fill = Brushes.Green;
                                    serverOneRebootButton.IsEnabled = true;
                                    serverOneInfoButton.IsEnabled = true;
                                });
                            }
                            else
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    serverOneStatus.Content = "No connection";
                                    serverOneIndicator.Fill = Brushes.Red;
                                    serverOneRebootButton.IsEnabled = false;
                                    serverOneInfoButton.IsEnabled = false;
                                });
                            }
                            client.Disconnect();
                        }
                        else
                        {
                            client = new SshClient(ip, user, pass);
                            bool connected = false;

                            try
                            {
                                client.Connect();
                                connected = true;
                            }
                            catch (Exception)
                            {
                                connected = false;
                            }

                            if (connected == true)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    serverOneStatus.Content = "Connected";
                                    serverOneIndicator.Fill = Brushes.Green;
                                    serverOneRebootButton.IsEnabled = true;
                                    serverOneInfoButton.IsEnabled = true;
                                });
                            }
                            else
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    serverOneStatus.Content = "No connection";
                                    serverOneIndicator.Fill = Brushes.Red;
                                    serverOneRebootButton.IsEnabled = false;
                                    serverOneInfoButton.IsEnabled = false;
                                });
                            }
                            client.Disconnect();
                        }
                    });
                }
            });
        }

        private void serverOneEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(0) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[0].ServerName;
                    editWindow.ServerTextBox.Text = servers[0].ServerIP;
                    editWindow.PortTextBox.Text = servers[0].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[0].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[0].ServerPassword;
                }

                editWindow.ShowDialog();

                if (editWindow.IsSaved)
                {
                    string tmpName = editWindow.ServerNameTextBox.Text;
                    string tmpIP = editWindow.ServerTextBox.Text;

                    int tmpPort = 0;
                    if (editWindow.PortTextBox.Text == "" || editWindow.PortTextBox.Text == "0")
                        tmpPort = 0;
                    else
                        tmpPort = Convert.ToInt32(editWindow.PortTextBox.Text);

                    string tmpPass = editWindow.PasswordTextBox.Text;
                    string tmpUser = editWindow.UserTextBox.Text;

                    Dispatcher.Invoke(() =>
                    {

                        var newServer = new Server(tmpName,tmpIP, tmpPort, tmpUser, tmpPass);
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

        private void serverOneRebootButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(()=> 
            {
            SshClient client;

                if (servers[0].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[0].ServerIP, Convert.ToInt32(servers[0].ServerPort), servers[0].ServerUsername, servers[0].ServerPassword);
                    
                    bool connected = false;
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[0].ServerPassword +"\n' | sudo -S reboot");
                        rebootCmd.Execute();
                        
                    }
                    client.Disconnect();
                }
                else
                {
                    client = new SshClient(servers[0].ServerIP, servers[0].ServerUsername, servers[0].ServerPassword);
                    bool connected = false;
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    if (connected == true) 
                    {
                        client.CreateCommand("echo -e '" + servers[0].ServerPassword + "\n' | sudo -S reboot");
                    }
                    client.Disconnect();
                }
            });
        }

        private void serverOneInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow();
                infoWindow.ShowDialog();
            });

            thread.Start();
        }
    }
}