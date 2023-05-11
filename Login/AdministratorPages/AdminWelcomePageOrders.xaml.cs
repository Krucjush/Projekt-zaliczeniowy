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
    /// This class is available only for "Administrator"s.
    /// It allows them to view, remove, and edit status of all placed orders.
    /// </summary>
    public partial class AdminWelcomePageOrders : Window
    {
        public string OrderStatus { get; set; }
        public bool IsSelected { get; set; }
        /// <summary>
        /// This constructor generates binding and fills "Orders" DataGrid.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows "AdminWelcomePageManageExpenses" window.
        /// </summary>
        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageExpenses();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows "AdminWelcomePageManageStocks" window.
        /// </summary>
        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageStocks();
                _.Show();
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
        /// This method opens "AdminWelcomePageManageAccounts" window.
        /// </summary>
        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageAccounts();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method logs user out, showing him login window.
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
        /// This method allows "Administrator"s to remove orders from database.
        /// If nothing is selected it shows a MessageBox.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method sets "OrderStatus" to "Pending".
        /// </summary>
        private void Pending_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Pending";
            IsSelected = true;
        }
        /// <summary>
        /// This method sets "OrderStatus" to "Processing".
        /// </summary>
        private void Processing_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Processing";
            IsSelected = true;
        }
        /// <summary>
        /// This method set "OrderStatus" to "Rejected".
        /// </summary>
        private void Rejected_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Rejected";
            IsSelected = true;
        }
        /// <summary>
        /// This method sets "OrderStatus" to "Completed".
        /// </summary>
        private void Completed_Selected(object sender, RoutedEventArgs e)
        {
            OrderStatus = "Completed";
            IsSelected = true;
        }
        /// <summary>
        /// This method allows "Administrator"s to set selected "OrderStatus" in database.
        /// If "OrderStatus" is not selected, "OrderStatus" is the same as the one in database or nothing is selected it shows a MessageBox.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method refreshes data, closing and reopening the window.
        /// </summary>
        private void Update()
        {
            var _ = new AdminWelcomePageOrders();
            _.Show();
            Close();
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
