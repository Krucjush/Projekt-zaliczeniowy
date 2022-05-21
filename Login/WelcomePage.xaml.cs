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
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Window
    {

        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Button_Click_Manage_Account(object sender, RoutedEventArgs e)
        {
            var manageAccount = new ManageAccount();
            manageAccount.Show();
        }
    }
}
