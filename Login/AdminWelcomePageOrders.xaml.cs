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
    /// Klasa do zarzadzania statusami zamowien
    /// Dostepna tylko dla administratorow
    /// </summary>
    public partial class AdminWelcomePageOrders : Window
    {
        public string OrderStatus { get; set; }
        public bool IsSelected { get; set; }
        public AdminWelcomePageOrders()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var _ = db.Orders
                    .Select(q => new OrdersTable { OrderId = q.OrderId, OrderStatus = q.OrderStatus, UserId = q.Id })
                    .ToList();
                Orders.ItemsSource = _;
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
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
            try
            {
                var row = (OrdersTable)Orders.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var selectedOrder = db.Orders
                            .Where(t => t.OrderId == row.OrderId)
                            .ToList()
                            .LastOrDefault();
                        db.Orders.Remove(selectedOrder);
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }
        private void Pending_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Pending";
            IsSelected = true;
        }

        private void Processing_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Processing";
            IsSelected = true;
        }
        private void Rejected_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Rejected";
            IsSelected = true;
        }
        private void Completed_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Completed";
            IsSelected = true;
        }
        private void ButtonClick_SetOrderStatus(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (OrdersTable)Orders.SelectedItem;
                
                if (!IsSelected)
                {
                    MessageBox.Show("New Order status was not selected");
                }
                else if (row == null)
                {
                    MessageBox.Show("No item selected");
                }
                else if (row.OrderStatus == OrderStatus)
                {
                    MessageBox.Show("No changes were made");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var order = db.Orders
                            .FirstOrDefault(q => q.OrderId == row.OrderId);
                        order.OrderStatus = OrderStatus;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
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
