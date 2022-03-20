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
    public partial class ControlPanelWindow : Window
    {
        // LIST TO STORE SAVED SERVERS:
        List<Server> servers = new List<Server>();

        public ControlPanelWindow()
        {
            InitializeComponent();

            // DISPATCH TIMER TO RUN SERVER STATUS CHECK EVERY SECOND
            TimeStart(0, 0, 10);
        }


        // -- DISPATCHER -- //

        // TIMER:
        private void TimeStart(int hours, int minutes, int seconds)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(hours, minutes, seconds);
            dispatcherTimer.Start();
        }

        // SERVER STATUS CHECKER:
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                foreach (var server in servers)
                {
                    Task.Run(() =>
                    {
                        // SERVER INFORMATION:
                        string ip = server.ServerIP;
                        string port = server.ServerPort.ToString();
                        string user = server.ServerUsername;
                        string pass = server.ServerPassword;

                        SshClient client;

                        bool connected = false;


                        // CHECK IF OUR SSH CLIENT USES A PORT AND CONNECT TO SERVER:
                        if (port != "" && port != "0")
                        {
                            client = new SshClient(ip, Convert.ToInt32(port), user, pass);
                        }
                        else
                        {
                            client = new SshClient(ip, user, pass);
                        }

                        // TRY CONNECT TO SERVER:
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
                            // SET STATUS TO CONNECTED AND INDICATOR GREEN - IF SERVER IS CONNNECTED
                            Dispatcher.Invoke(() =>
                            {
                                if (servers.IndexOf(server) == 0)
                                {
                                    serverOneStatus.Content = "Connected";
                                    serverOneIndicator.Fill = Brushes.Green;
                                    serverOneRebootButton.IsEnabled = true;
                                    serverOneInfoButton.IsEnabled = true;
                                }
                                else if (servers.IndexOf(server) == 1)
                                {
                                    serverTwoStatus.Content = "Connected";
                                    serverTwoIndicator.Fill = Brushes.Green;
                                    serverTwoRebootButton.IsEnabled = true;
                                    serverTwoInfoButton.IsEnabled = true;
                                }
                                else if (servers.IndexOf(server) == 2)
                                {
                                    serverThreeStatus.Content = "Connected";
                                    serverThreeIndicator.Fill = Brushes.Green;
                                    serverThreeRebootButton.IsEnabled = true;
                                    serverThreeInfoButton.IsEnabled = true;
                                }
                                else if (servers.IndexOf(server) == 3)
                                {
                                    serverFourStatus.Content = "Connected";
                                    serverFourIndicator.Fill = Brushes.Green;
                                    serverFourRebootButton.IsEnabled = true;
                                    serverFourInfoButton.IsEnabled = true;
                                }
                                else if (servers.IndexOf(server) == 4)
                                {
                                    serverFiveStatus.Content = "Connected";
                                    serverFiveIndicator.Fill = Brushes.Green;
                                    serverFiveRebootButton.IsEnabled = true;
                                    serverFiveInfoButton.IsEnabled = true;
                                }
                                else if (servers.IndexOf(server) == 5)
                                {
                                    serverSixStatus.Content = "Connected";
                                    serverSixIndicator.Fill = Brushes.Green;
                                    serverSixRebootButton.IsEnabled = true;
                                    serverSixInfoButton.IsEnabled = true;
                                }
                            });
                        }
                        else
                        {
                            // SET STATUS TO CONNECTED AND INDICATOR RED - IF SERVER IS NOT CONNNECTED
                            Dispatcher.Invoke(() =>
                            {
                                if (servers.IndexOf(server) == 0)
                                {
                                    serverOneStatus.Content = "No connection";
                                    serverOneIndicator.Fill = Brushes.Red;
                                    serverOneRebootButton.IsEnabled = false;
                                    serverOneInfoButton.IsEnabled = false;
                                }
                                else if (servers.IndexOf(server) == 1)
                                {
                                    serverTwoStatus.Content = "No connection";
                                    serverTwoIndicator.Fill = Brushes.Red;
                                    serverTwoRebootButton.IsEnabled = false;
                                    serverTwoInfoButton.IsEnabled = false;
                                }
                                else if (servers.IndexOf(server) == 2)
                                {
                                    serverThreeStatus.Content = "No connection";
                                    serverThreeIndicator.Fill = Brushes.Red;
                                    serverThreeRebootButton.IsEnabled = false;
                                    serverThreeInfoButton.IsEnabled = false;
                                }
                                else if (servers.IndexOf(server) == 3)
                                {
                                    serverFourStatus.Content = "No connection";
                                    serverFourIndicator.Fill = Brushes.Red;
                                    serverFourRebootButton.IsEnabled = false;
                                    serverFourInfoButton.IsEnabled = false;
                                }
                                else if (servers.IndexOf(server) == 4)
                                {
                                    serverFiveStatus.Content = "No connection";
                                    serverFiveIndicator.Fill = Brushes.Red;
                                    serverFiveRebootButton.IsEnabled = false;
                                    serverFiveInfoButton.IsEnabled = false;
                                }
                                else if (servers.IndexOf(server) == 5)
                                {
                                    serverSixStatus.Content = "No connection";
                                    serverSixIndicator.Fill = Brushes.Red;
                                    serverSixRebootButton.IsEnabled = false;
                                    serverSixInfoButton.IsEnabled = false;
                                }
                            });
                        }
                        client.Disconnect();
                    });
                }
            });
        }


        // -- BUTTONS -- //

        // REBOOT BUTTONS:
        private void serverOneRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[0].ServerPort != 0 && servers[0].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[0].ServerIP, Convert.ToInt32(servers[0].ServerPort), servers[0].ServerUsername, servers[0].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[0].ServerIP, servers[0].ServerUsername, servers[0].ServerPassword);
                }

                if (servers[0].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[0].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }

                }
            });
        }

        private void serverTwoRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[1].ServerPort != 0 && servers[1].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[1].ServerIP, Convert.ToInt32(servers[1].ServerPort), servers[1].ServerUsername, servers[1].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[1].ServerIP, servers[1].ServerUsername, servers[1].ServerPassword);
                }

                if (servers[1].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[1].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }
                }
            });
        }

        private void serverThreeRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[2].ServerPort != 0 && servers[2].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[2].ServerIP, Convert.ToInt32(servers[2].ServerPort), servers[2].ServerUsername, servers[2].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[2].ServerIP, servers[2].ServerUsername, servers[2].ServerPassword);
                }

                if (servers[2].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[2].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }
                }
            });
        }

        private void serverFourRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[3].ServerPort != 0 && servers[3].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[3].ServerIP, Convert.ToInt32(servers[3].ServerPort), servers[3].ServerUsername, servers[3].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[3].ServerIP, servers[3].ServerUsername, servers[3].ServerPassword);
                }

                if (servers[3].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[3].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }
                }
            });
        }

        private void serverFiveRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[4].ServerPort != 0 && servers[4].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[4].ServerIP, Convert.ToInt32(servers[4].ServerPort), servers[4].ServerUsername, servers[4].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[4].ServerIP, servers[4].ServerUsername, servers[4].ServerPassword);
                }

                if (servers[4].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[4].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }
                }
            });
        }

        private void serverSixRebootButton_Click(object sender, RoutedEventArgs e)
        {
            // RUNS A TASK THAT TRIES TO CONNECT TO THE SERVER AND SEND A REBOOT A COMMAND
            Task.Run(() =>
            {
                SshClient client;
                bool connected = false;

                // CHECK IF WE'RE USING A PORT OR NOT
                if (servers[5].ServerPort != 0 && servers[5].ServerPort.ToString() != "")
                {
                    client = new SshClient(servers[5].ServerIP, Convert.ToInt32(servers[5].ServerPort), servers[5].ServerUsername, servers[5].ServerPassword);
                }
                else
                {
                    client = new SshClient(servers[5].ServerIP, servers[5].ServerUsername, servers[5].ServerPassword);
                }

                if (servers[5].ServerPort.ToString() != "")
                {
                    try
                    {
                        client.Connect();
                        connected = true;
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    // IF WE'RE CONNECTED, TRY RUN THE REBOOT COMMAND
                    if (connected == true)
                    {
                        var rebootCmd = client.CreateCommand("echo -e '" + servers[5].ServerPassword + "' | sudo -S reboot");
                        try
                        {
                            rebootCmd.Execute();
                        }
                        catch (Exception)
                        {
                            // EXCEPTION WILL APPEAR, WHEN THE SERVER REBOOTS WE LOOSE CONNECTION, WHICH CAUSES AN EXCEPTION
                        }
                        client.Disconnect();
                        connected = false;
                    }
                }
            });
        }


        // INFO BUTTONS:
        private void serverOneInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[0]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverTwoInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[1]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverThreeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[2]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverFourInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[3]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverFiveInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[4]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverSixInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                var infoWindow = new infoWindow(servers[5]);
                infoWindow.ShowDialog();
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }


        // EDIT BUTTONS:
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
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

        private void serverTwoEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(1) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[1].ServerName;
                    editWindow.ServerTextBox.Text = servers[1].ServerIP;
                    editWindow.PortTextBox.Text = servers[1].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[1].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[1].ServerPassword;
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(1) == null)
                            servers.Add(newServer);
                        else
                            servers[1] = newServer;

                        serverTwoName.Content = servers[1].ServerName;
                        if (servers[1].ServerPort.ToString() != "")
                            serverTwoIP.Content = servers[1].ServerIP + ":" + servers[1].ServerPort;
                        else
                            serverTwoIP.Content = servers[1].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverThreeEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(2) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[2].ServerName;
                    editWindow.ServerTextBox.Text = servers[2].ServerIP;
                    editWindow.PortTextBox.Text = servers[2].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[2].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[2].ServerPassword;
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(2) == null)
                            servers.Add(newServer);
                        else
                            servers[2] = newServer;

                        serverThreeName.Content = servers[2].ServerName;
                        if (servers[2].ServerPort.ToString() != "")
                            serverThreeIP.Content = servers[2].ServerIP + ":" + servers[2].ServerPort;
                        else
                            serverThreeIP.Content = servers[2].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverFourEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(3) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[3].ServerName;
                    editWindow.ServerTextBox.Text = servers[3].ServerIP;
                    editWindow.PortTextBox.Text = servers[3].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[3].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[3].ServerPassword;
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(3) == null)
                            servers.Add(newServer);
                        else
                            servers[3] = newServer;

                        serverFourName.Content = servers[3].ServerName;
                        if (servers[3].ServerPort.ToString() != "")
                            serverFourIP.Content = servers[3].ServerIP + ":" + servers[3].ServerPort;
                        else
                            serverFourIP.Content = servers[3].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverFiveEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(4) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[4].ServerName;
                    editWindow.ServerTextBox.Text = servers[4].ServerIP;
                    editWindow.PortTextBox.Text = servers[4].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[4].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[4].ServerPassword;
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(4) == null)
                            servers.Add(newServer);
                        else
                            servers[4] = newServer;

                        serverFiveName.Content = servers[4].ServerName;
                        if (servers[4].ServerPort.ToString() != "")
                            serverFiveIP.Content = servers[4].ServerIP + ":" + servers[4].ServerPort;
                        else
                            serverFiveIP.Content = servers[4].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void serverSixEditButton_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                CreateWindow editWindow = new CreateWindow();

                if (servers.ElementAtOrDefault(1) != null)
                {
                    editWindow.ServerNameTextBox.Text = servers[5].ServerName;
                    editWindow.ServerTextBox.Text = servers[5].ServerIP;
                    editWindow.PortTextBox.Text = servers[5].ServerPort.ToString();
                    editWindow.UserTextBox.Text = servers[5].ServerUsername;
                    editWindow.PasswordTextBox.Text = servers[5].ServerPassword;
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

                        var newServer = new Server(tmpName, tmpIP, tmpPort, tmpUser, tmpPass);
                        if (servers.ElementAtOrDefault(5) == null)
                            servers.Add(newServer);
                        else
                            servers[5] = newServer;

                        serverSixName.Content = servers[5].ServerName;
                        if (servers[5].ServerPort.ToString() != "")
                            serverSixIP.Content = servers[5].ServerIP + ":" + servers[5].ServerPort;
                        else
                            serverSixIP.Content = servers[5].ServerIP;
                    });

                }
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }


    }
}