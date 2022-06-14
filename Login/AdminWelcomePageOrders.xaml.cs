using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
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
            DataContext = this;
            using var db = new UsersContext();
            var _ = db.Orders
                .ToList();
            Orders.ItemsSource = _;
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
        private void ButtonClick_ManageProducts(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageManageProducts();
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
        private void ButtonClick_RemoveOrder(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Order)Orders.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected.");
            }
            else
            {
                var selectedOrder = db.Orders
                    .Where(t => t.OrderId == row.OrderId)
                    .ToList()
                    .LastOrDefault();
                db.Orders.Remove(selectedOrder);
                db.SaveChanges();
                Update();
            }
        }
        private void Pending_Selected(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Order)Orders.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedOrder = db.Orders
                    .Where(o => o.OrderId == row.OrderId)
                    .ToList()
                    .LastOrDefault();
                selectedOrder.OrderStatus = "Pending";
                db.SaveChanges();
                Update();
            }
        }

        private void Processing_Selected(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Order)Orders.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedOrder = db.Orders
                    .Where(o => o.OrderId == row.OrderId)
                    .ToList()
                    .LastOrDefault();
                selectedOrder.OrderStatus = "Pending";
                db.SaveChanges();
                Update();
            }
        }
        private void Rejected_Selected(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Order)Orders.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedOrder = db.Orders
                    .Where(o => o.OrderId == row.OrderId)
                    .ToList()
                    .LastOrDefault();
                selectedOrder.OrderStatus = "Rejected";
                db.SaveChanges();
                Update();
            }
        }
        private void Completed_Selected(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Order)Orders.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedOrder = db.Orders
                    .Where(o => o.OrderId == row.OrderId)
                    .ToList()
                    .LastOrDefault();
                selectedOrder.OrderStatus = "Completed";
                db.SaveChanges();
                Update();
            }
        }
        private void Update()
        {
            var _ = new AdminWelcomePageOrders();
            _.Show();
            Close();
        }
    }
}
