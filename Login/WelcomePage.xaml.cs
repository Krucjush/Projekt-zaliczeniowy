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
        public List<Product> Products;
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public List<ProductInStore> ProductsInStore { get; set; }
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = this;
            using (var db = new UsersContext())
            {
                var _ = db.Stocks
                    .Select(q => new { q.ItemName, q.Quantity })
                    .ToList();
                Store.ItemsSource = _;
            }
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
            var row = (Product)Store.SelectedItem;
            var names = Products
                .Select(cart => cart.ProductName)
                .ToList();
            if (Amount < 1)
            {
                MessageBox.Show("You cannot add less than one item.");
            }
            else if (row == null)
            {
                MessageBox.Show("No item selected");
            }
            else if (names.Contains(row.ProductName))
            {
                var i = Products
                    .LastOrDefault(q => q.ProductName == row.ProductName);
                using (var db = new UsersContext())
                {
                    var t = db.Stocks
                        .LastOrDefault(q => q.ItemName == row.ProductName);
                    if (t.Quantity >= Amount)
                    {
                        i.Amount += Amount;
                        t.Quantity -= Amount;
                        db.SaveChanges();
                        Update();
                    }
                    else
                    {
                        MessageBox.Show("Not enough items in the store");
                    }
                }
            }
            else
            {
                using (var db = new UsersContext())
                {
                    var t = db.Stocks
                        .LastOrDefault(q => q.ItemName == row.ProductName);
                    if (t.Quantity < Amount) return;
                    Products.Add(new Product { Amount = Amount, ProductName = ProductName });
                    t.Quantity -= Amount;
                    db.SaveChanges();
                    Update();
                }
            }
        }
        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            var row = (ProductInStore)Store.SelectedItem;
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
                using (var db = new UsersContext())
                {
                    var t = db.Stocks
                        .LastOrDefault(q => q.ItemName == row.ProductName);
                    t.Quantity += Amount;
                    db.SaveChanges();
                }
                i.Amount -= Amount;
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
