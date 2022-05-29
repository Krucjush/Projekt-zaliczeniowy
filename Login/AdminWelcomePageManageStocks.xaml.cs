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

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWelcomePageManageStocks.xaml
    /// </summary>
    public partial class AdminWelcomePageManageStocks : Window
    {
        public AdminWelcomePageManageStocks()
        {
            InitializeComponent();
        }

        private void Button_Click_Manage_Expenses(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }

        private void Button_Click_Manage_Accounts(object sender, RoutedEventArgs e)
        {

        }
    }
}
