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
    /// Logika interakcji dla klasy AdminWelcomePageOrders.xaml
    /// </summary>
    public partial class AdminWelcomePageOrders : Window
    {
        public AdminWelcomePageOrders()
        {
            InitializeComponent();
        }

        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageManageExpenses();
            _.Show();
            Close();
        }

        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageManageStocks();
            _.Show();
            Close();
        }

        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageManageAccounts();
            _.Show();
            Close();
        }

        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }

        private void ButtonClick_AddOrder(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
