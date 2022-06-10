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
using Microsoft.EntityFrameworkCore.Query.Internal;
using Program;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWelcomePage.xaml
    /// </summary>
    public partial class AdminWelcomePage : Window
    {
        public AdminWelcomePage()
        {
            InitializeComponent();
        }
        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }
        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageStocks();
            q.Show();
            Close();
        }
        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageAccounts();
            q.Show();
            Close();
        }

        private void ButtonClick_Orders(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageOrders();
            _.Show();
            Close();
        }

        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }
    }
}
