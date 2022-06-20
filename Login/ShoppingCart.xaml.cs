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
    /// Logika interakcji dla klasy ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window
    {
        public bool DoClose { get; set; } = true;
        public List<CartItem> CartItems { get; set; }
        public ShoppingCart(List<CartItem> cartItems)
        {
            InitializeComponent();
            CartItems = cartItems;
            Products.ItemsSource = CartItems;
        }

        private void ButtonClick_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var row = (CartItem)Products.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
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
                    CartItems.Remove(row);
                    DoClose = false;
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }
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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }
        private void Update()
        {
            var _ = new ShoppingCart(CartItems);
            _.Show();
            Close();
        }
        private void ShoppingCart_Closing(object sender, CancelEventArgs e)
        {
            if(!DoClose) return;
            var _ = new WelcomePage();
            _.Show();
        }
    }
}
