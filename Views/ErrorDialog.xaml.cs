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

namespace MeterReadingApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : Window
    {
        public ErrorDialog(string message)
        {
            InitializeComponent();
            MessageBox.Text = message;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
