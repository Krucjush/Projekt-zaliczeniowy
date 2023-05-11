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
    /// This class is shown after successful "Administrator" login.
    /// It is used only for navigation.
    /// </summary>
    public partial class AdminWelcomePage : Window
    {
        public AdminWelcomePage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageExpenses" window.
        /// </summary>
        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = new AdminWelcomePageManageExpenses();
                q.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageStocks" window.
        /// </summary>
        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = new AdminWelcomePageManageStocks();
                q.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageAccounts" window.
        /// </summary>
        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = new AdminWelcomePageManageAccounts();
                q.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageProducts" window.
        /// </summary>
        private void ButtonClick_ManageProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageProducts();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageOrders" window.
        /// </summary>
        private void ButtonClick_Orders(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageOrders();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method logs user out, and opens login window.
        /// </summary>
        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new LoginWindow();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows user a message, about error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
