using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UbuntuPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine();
        }

        private void MorinfoBtn_Click(object sender, RoutedEventArgs e)
        {

            //Rectangle - ServerTextBox
            Rectangle rectangle = new Rectangle();
            var color = new BrushConverter();
            rectangle.Fill = (Brush)color.ConvertFrom("#FFD9D9DF");
            rectangle.Height = 154;
            rectangle.Width = 250;
            rectangle.VerticalAlignment = 0;
            rectangle.HorizontalAlignment = 0;
            rectangle.Margin = new Thickness(20, 80, 0, 0);
            minGrid.Children.Add(rectangle);

            //Label--Navn - ServerTextBox
            Label ServerNavnLabel = new Label();
            ServerNavnLabel.Content = "Navn - Server";
            ServerNavnLabel.Name = "ServerNavnLabel";
            ServerNavnLabel.Margin = new Thickness(100, 90, 0, 0);
            ServerNavnLabel.FontSize = 14;
            ServerNavnLabel.FontWeight = FontWeights.Bold;
            minGrid.Children.Add(ServerNavnLabel);

            //Label--StatusTextBox
            Label StatusLabel = new Label();
            StatusLabel.Content = "Status : ";
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Margin = new Thickness(40, 120, 0, 0);
            StatusLabel.FontSize = 14;
            StatusLabel.FontWeight = FontWeights.Bold;
            minGrid.Children.Add(StatusLabel);

            //Ellipse
            Ellipse ellipse = new Ellipse();
            ellipse.Name = "ellipse";
            ellipse.Margin = new Thickness(220, 115, 0, 0);
            ellipse.Height = 20;
            ellipse.Width = 20;
            ellipse.Fill = Brushes.Green;
            ellipse.VerticalAlignment = 0;
            ellipse.HorizontalAlignment = 0;
            minGrid.Children.Add(ellipse);

            //Label--IPTextBox
            Label IPLabel = new Label();
            IPLabel.Content = "IP : ";
            IPLabel.Name = "IPLabel";
            IPLabel.Margin = new Thickness(40, 155, 0, 0);
            IPLabel.FontSize = 14;
            IPLabel.FontWeight = FontWeights.Bold;
            minGrid.Children.Add(IPLabel);

            //Label--IPoput
            Label IpTextbox = new Label();
            IpTextbox.Content = "--80";
            IpTextbox.Name = "IpTextbox";
            IpTextbox.Margin = new Thickness(175, 155, 0, 0);
            IpTextbox.FontSize = 14;
            IpTextbox.FontWeight = FontWeights.Bold;
            minGrid.Children.Add(IpTextbox);

            //RebootButton 
            Button RebootBtn = new Button();
            RebootBtn.Width = 95;
            RebootBtn.Height = 24;
            RebootBtn.Name = "RebootBtn";
            RebootBtn.Content = "Reboot";
            RebootBtn.Foreground = Brushes.White;
            RebootBtn.Background = Brushes.Black;
            RebootBtn.FontSize = 14;
            RebootBtn.Margin = new Thickness(40, 200, 0, 0);
            RebootBtn.FontWeight = FontWeights.Bold;
            RebootBtn.VerticalAlignment = 0;
            RebootBtn.HorizontalAlignment = 0;
            minGrid.Children.Add(RebootBtn);


            //RebootButton 
            Button MorinfoBtn = new Button();
            MorinfoBtn.Width = 95;
            MorinfoBtn.Height = 24;
            MorinfoBtn.Name = "MorinfoBtn";
            MorinfoBtn.Content = "Morinfo";
            MorinfoBtn.Foreground = Brushes.White;
            MorinfoBtn.Background = Brushes.Black;
            MorinfoBtn.FontSize = 14;
            MorinfoBtn.Margin = new Thickness(150, 200, 0, 0);
            MorinfoBtn.FontWeight = FontWeights.Bold;
            MorinfoBtn.VerticalAlignment = 0;
            MorinfoBtn.HorizontalAlignment = 0;
            minGrid.Children.Add(MorinfoBtn);


        }
    }
}
