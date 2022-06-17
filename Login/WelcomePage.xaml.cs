using System;
using System.Collections.Generic;
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
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = this;
            using var db = new UsersContext();
            var _ = db.Products
                .Select(q => new ProductInStore { Available = q.Stock.Quantity, ProductName = q.ProductName, Price = q.Price, AmountInCart = q.OrderItems.Where(p => p.ProductId == q.ProductId && p.Orders.OrderStatus == "Pending").Select(p => p.Quantity).FirstOrDefault()})
                .ToList();
            Store.ItemsSource = _;
            var c = db.Products
                .Where(q => q.OrderItems.Select(p => p.Orders.OrderStatus).FirstOrDefault() == "Pending")
                .Select(q => new CartItem { Amount = q.OrderItems.Where(p => p.ProductId == q.ProductId).Select(p => p.Quantity).FirstOrDefault(), ProductName = q.ProductName, Cost = q.Price, TotalCost = (float)Math.Round(q.Price * q.OrderItems.Where(p => p.ProductId == q.ProductId).Select(p => p.Quantity).FirstOrDefault(), 2) })
                .ToList();
            CartItems = c;
        }

        private void ButtonClick_ManageAccount(object sender, RoutedEventArgs e)
        {
            var _ = new ManageAccount();
            _.Show();
            Close();
        }

        private void ButtonClick_ShoppingCart(object sender, RoutedEventArgs e)
        {
            var _ = new ShoppingCart(CartItems);
            _.Show();
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
                    //.Where(q => q.OrderItems.Select(r => r.Orders.OrderStatus).FirstOrDefault() == "Pending")
                    .FirstOrDefault(q => q.ProductName == row.ProductName);
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
                    var s = db.Stocks
                        .FirstOrDefault(q => q.StockId == p.StockId);
                    s.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
                else
                {
                    var orderItem = db.OrderItems
                        .FirstOrDefault(q => q.ProductId == p.ProductId);
                    var s = db.Stocks
                        .FirstOrDefault(q => q.StockId == p.StockId);
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
                    var p = db.Products
                        .FirstOrDefault(q => q.ProductName == row.ProductName);
                    var s = db.Stocks
                        .FirstOrDefault(q => q.StockId == p.StockId);
                    var orderItem = db.OrderItems
                        .FirstOrDefault(q => q.ProductId == p.ProductId);
                    db.OrderItems.Remove(orderItem);
                    s.Quantity += (long)row.AmountInCart;
                    db.SaveChanges();
                    Update();
                    break;
                }
                default:
                {
                    var p = db.Products
                        .FirstOrDefault(q => q.ProductName == row.ProductName);
                    var s = db.Stocks
                        .FirstOrDefault(q => q.StockId == p.StockId);
                    var orderItem = db.OrderItems
                        .FirstOrDefault(q => q.ProductId == p.ProductId);
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
            if (o.Contains("Pending") && (user.Address == null || user.ZipCode == null))
            {
                MessageBox.Show("Please fill additional data.");
                var _ = new ManageAccount();
                _.Show();
                Close();
            }
            else if (o.Contains("Pending"))
            {
                var order = db.Orders
                    .Where(q => q.Id == user.Id)
                    .FirstOrDefault(q => q.OrderStatus == "Pending");
                order.OrderStatus = "Processing";
                db.SaveChanges();
                MessageBox.Show("Currently only cash on delivery is available.");
                CartItems = null;
                Update();
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
            Close();
        }
    }
}
