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

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWelcomePageManageProducts.xaml
    /// </summary>
    public partial class AdminWelcomePageManageProducts : Window
    {
        public float Price { get; set; }
        public AdminWelcomePageManageProducts()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var d = db.Products
                    .Select(q => new ProductTable { ProductId = q.ProductId, ProductName = q.ProductName, Price = q.Price })
                    .ToList();
                Products.ItemsSource = d;
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }
        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }
        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageStocks();
            q.Show();
            Close();
        }
        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageAccounts();
            q.Show();
            Close();
        }
        private void ButtonClick_Orders(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageOrders();
            _.Show();
            Close();
        }
        private void ButtonClick_LogOut(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }
        private void ButtonClick_SetPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (ProductTable)Products.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else if (Price == 0)
                {
                    MessageBox.Show("Price cannot be 0.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var selectedProduct = db.Products
                            .Where(p => p.ProductId == row.ProductId)
                            .ToList()
                            .LastOrDefault();
                        selectedProduct.Price = (float)Math.Round(Price, 2);
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
        public void Update()
        {
            var _ = new AdminWelcomePageManageProducts();
            _.Show();
            Close();
        }
    }
}
