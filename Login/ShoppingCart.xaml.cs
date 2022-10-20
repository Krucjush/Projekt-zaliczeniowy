using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// This class is only accessible by Customers.
    /// It is used to show user current state of their shopping cart, i.e Amount, ProductName, Cost and TotalCost.
    /// </summary>
    public partial class ShoppingCart : Window
    {
        public bool DoClose { get; set; } = true;
        public List<CartItem> CartItems { get; set; }
        /// <summary>
        /// This constructor generates binding and fills Products DataGrid.
        /// </summary>
        /// <param name="cartItems">Items that are currently in users shopping cart</param>
        public ShoppingCart(List<CartItem> cartItems)
        {
            try
            {
                InitializeComponent();
                CartItems = cartItems;
                Products.ItemsSource = CartItems;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method closes ShoppingCart window.
        /// </summary>
        private void ButtonClick_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// This method removes selected item from the shopping cart.
        /// If nothing is selected it shows a MessageBox.
        /// </summary>
        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (CartItem)Products.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var p = db.Products
                            .FirstOrDefault(q => q.ProductName == row.ProductName);
                        var s = db.Stocks
                            .FirstOrDefault(q => q.StockId == p.StockId);
                        var orderItem = db.OrderItems
                            .FirstOrDefault(q => q.ProductId == p.ProductId);
                        db.OrderItems.Remove(orderItem);
                        s.Quantity += row.Amount;
                        db.SaveChanges();
                    }
                    CartItems.Remove(row);
                    DoClose = false;
                    Update();
                }
            }
            catch(Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows user OrderInformation window.
        /// If user hasn't given needed data for delivery it shows a MessageBox, and opens ManageAccount window.
        /// If nothing is in cart it shows a MessageBox.
        /// </summary>
        private void ButtonClick_Order(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var user = db.UserLogins
                    .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
                var o = db.Orders
                    .Where(q => q.Id == user.Id)
                    .Select(q => q.OrderStatus)
                    .ToList();
                if (o.Contains("Pending") && (user.Address == null || user.ZipCode == null || user.FirstName == null || user.LastName == null))
                {
                    MessageBox.Show("Please fill additional data.");
                    var _ = new ManageAccount();
                    _.Show();
                    DoClose = false;
                    Close();
                }
                else if (o.Contains("Pending"))
                {
                    var _ = new OrderInformation();
                    _.Show();
                    DoClose = false;
                    Close();
                }
                else
                {
                    MessageBox.Show("You don't have any items in cart.");
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method refreshes data, by closing and reopening window.
        /// </summary>
        private void Update()
        {
            var _ = new ShoppingCart(CartItems);
            _.Show();
            Close();
        }
        /// <summary>
        /// This method checks if user actually intends to close the window.
        /// If so it opens WelcomePage.
        /// </summary>
        private void ShoppingCart_Closing(object sender, CancelEventArgs e)
        {
            if(!DoClose) return;
            var _ = new WelcomePage();
            _.Show();
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            DoClose = false;
            Close();
        }
    }
}
