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
using System.Windows.Shapes;

namespace UbuntuPanel
{
    public partial class CreateWindow : Window
    {
        bool Saved = false;

        public CreateWindow()
        {
            InitializeComponent();
        }

        public bool IsSaved
        {
            get { return Saved; }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Saved = true;
            this.Close();
        }

        private void ServerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
