using System;
using System.Collections.Generic;
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
        public List<ProductInStore> Products;
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = this;
            using var db = new UsersContext();
            var _ = db.Products
                .Select(q => new ProductInStore{ Amount = q.Stock.Quantity, ProductName = q.ProductName, Price = q.Price })
                .ToList();
            Store.ItemsSource = _;
        }

        private void ButtonClick_ManageAccount(object sender, RoutedEventArgs e)
        {
            Close();
            var _ = new ManageAccount();
            _.Show();
        }

        private void ButtonClick_ShoppingCart(object sender, RoutedEventArgs e)
        {
            Close();
            var _ = new ShoppingCart(Products);
            _.Show();
        }

        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            Close();
            var _ = new LoginWindow();
            _.Show();
        }

        private void ButtonClick_Add(object sender, RoutedEventArgs e)
        {
            var row = (ProductInStore)Store.SelectedItem;
            using var db = new UsersContext();
            var o = db.OrderItems
                 .LastOrDefault(q => q.Products.ProductName == row.ProductName);
            if (Amount < 1)
            {
                MessageBox.Show("You cannot add less than one item.");
            }
            else if (row == null)
            {
                MessageBox.Show("No item selected");
            }
            else if (row.AmountInCart > 0)
            {
                var i = Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                var t = db.Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                if (t.Stock.Quantity >= Amount)
                {
                    i.Amount += Amount;
                    i.AmountInCart += Amount;
                    t.Stock.Quantity -= Amount;
                    o.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
                else
                {
                    MessageBox.Show("Not enough items in the store.");
                }
            }
            else
            {
                var t = db.Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                if (t.Stock.Quantity >= Amount)
                {
                    Products.Add(new ProductInStore { Amount = Amount, ProductName = ProductName, Price = row.Price, AmountInCart = Amount});
                    t.Stock.Quantity -= Amount;
                    o.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
                else
                {
                    MessageBox.Show("Not enough items in the store.");
                }
            }
        }
        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            var row = (ProductInStore)Store.SelectedItem;
            using var db = new UsersContext();
            var o = db.OrderItems
                .LastOrDefault(q => q.Products.ProductName == row.ProductName);
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else if (Amount < 0)
            {
                MessageBox.Show("You cannot remove less than one item.");
            }
            else if (row.Amount < Amount)
            {
                MessageBox.Show("You cannot remove more items than you have in cart.");
            }
            else
            {
                var i = Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                var t = db.Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                t.Stock.Quantity += Amount;
                i.Amount -= Amount;
                o.Quantity += Amount;
                db.SaveChanges();
                Update();
            }
        }
        private void Update()
        {
            Close();
            var _ = new WelcomePage();
            _.Show();
        }
    }
}
