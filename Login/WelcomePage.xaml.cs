using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Window
    {
        public List<CartItem> CartItems { get; set; }
        public int Amount { get; set; }
        public bool DoClose { get; set; } = true;
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = this;
            using var db = new UsersContext(); 
            var _ = db.Products
                .Select(q => new ProductInStore { Available = q.Stock.Quantity, ProductName = q.ProductName, Price = q.Price, AmountInCart = q.OrderItems.Where(p => p.ProductId == q.ProductId && p.Orders.OrderStatus == "Pending").Select(p => p.Quantity).FirstOrDefault()})
                .ToList();
            Store.ItemsSource = _;
            var c = db.OrderItems
                .Where(q => q.Orders.OrderStatus == "Pending")
                .Select(q => new CartItem
                {
                    Amount = q.Quantity, ProductName = q.Products.ProductName, Cost = q.Price,
                    TotalCost = (float)Math.Round(q.Price * q.Quantity, 2)
                })
                .ToList();
            CartItems = c;
        }

        private void ButtonClick_ManageAccount(object sender, RoutedEventArgs e)
        {
            var _ = new ManageAccount();
            _.Show();
            DoClose = false;
            Close();
        }

        private void ButtonClick_ShoppingCart(object sender, RoutedEventArgs e)
        {
            var _ = new ShoppingCart(CartItems);
            _.Show();
            DoClose = false;
            Close();
        }

        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            var row = (ProductInStore)Store.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("No item selected");
            }
            else
            {
                using var db = new UsersContext();
                var user = db.UserLogins
                    .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
                var o = db.Orders
                    .Where(q => q.Id == user.Id)
                    .Select(q => q.OrderStatus)
                    .ToList();
                var p = db.Products
                    .FirstOrDefault(q => q.ProductName == row.ProductName);
                var s = db.Stocks
                    .FirstOrDefault(q => q.StockId == p.StockId);
                if (!o.Contains("Pending"))
                {
                    var order = new Order { OrderStatus = "Pending", Id = user.Id, StockId = p.StockId };
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                if (Amount < 1)
                {
                    MessageBox.Show("You cannot add less than one item.");
                }
                else if (Amount > row.Available)
                {
                    MessageBox.Show("Not enough items in store.");
                }
                else if (row.AmountInCart == 0)
                {

                    var order = db.Orders
                        .Where(q => q.Id == user.Id)
                        .FirstOrDefault(q => q.OrderStatus == "Pending");
                    db.OrderItems.Add(new OrderItem
                        { OrderId = order.OrderId, Quantity = Amount, Price = p.Price, ProductId = p.ProductId });
                    s.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
                else
                {
                    var orderItem = db.OrderItems
                        .Where(q => q.Orders.OrderStatus == "Pending")
                        .FirstOrDefault(q => q.ProductId == p.ProductId);
                    orderItem.Quantity += Amount;
                    s.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
            }
        }
        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            var row = (ProductInStore)Store.SelectedItem;
            using var db = new UsersContext();
            var p = db.Products
                .FirstOrDefault(q => q.ProductName == row.ProductName);
            var s = db.Stocks
                .FirstOrDefault(q => q.StockId == p.StockId);
            var orderItem = db.OrderItems
                .Where(q => q.Orders.OrderStatus == "Pending")
                .FirstOrDefault(q => q.ProductId == p.ProductId);
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else if (row.AmountInCart == 0)
            {
                MessageBox.Show("You don't have this item in cart.");
            }
            else switch (Amount)
            {
                case < 0:
                    MessageBox.Show("You cannot remove negative number of items.");
                    break;
                case 0:
                {

                    db.OrderItems.Remove(orderItem);
                    s.Quantity += (long)row.AmountInCart;
                    db.SaveChanges();
                    Update();
                    break;
                }
                default:
                {
                    if (Amount > orderItem.Quantity)
                    {
                        MessageBox.Show("You don't have this many items in cart.");
                    }
                    else if (Amount == orderItem.Quantity)
                    {
                        db.OrderItems.Remove(orderItem);
                        s.Quantity += Amount;
                        db.SaveChanges();
                        Update();
                    }
                    else
                    {
                        orderItem.Quantity -= Amount;
                        s.Quantity += Amount;
                        db.SaveChanges();
                        Update();
                    }
                    break;
                }
            }
        }
        private void ButtonClick_Order(object sender, RoutedEventArgs e)
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
        private void Update()
        {
            var _ = new WelcomePage();
            _.Show();
            DoClose = false;
            Close();
        }

        private void WelcomePage_OnClosing(object sender, CancelEventArgs e)
        {
            if(!DoClose) return;
            using var db = new UsersContext();
            var user = db.UserLogins
                .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
            var order = db.Orders
                .Where(q => q.Id == user.Id)
                .FirstOrDefault(q => q.OrderStatus == "Pending");
            if (order == null) return;
            order.OrderStatus = "Rejected";
            order.Payment = false;
            db.SaveChanges();
        }
    }
}
