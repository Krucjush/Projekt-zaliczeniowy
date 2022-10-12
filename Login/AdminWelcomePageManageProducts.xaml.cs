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
    /// This class is available only for "Administrator"s.
    /// It allows them to view and change price of products.
    /// </summary>
    public partial class AdminWelcomePageManageProducts : Window
    {
        public float Price { get; set; }
        /// <summary>
        /// This constructor generates binding, and fills "Products" DataGrid.
        /// </summary>
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
                var q = new AdminWelcomePageManageExpenses();
                q.Show();
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
                var q = new AdminWelcomePageManageStocks();
                q.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        /// <summary>
        /// This method shows "AdminWelcomePageManageAccounts" window.
        /// </summary>
        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                var q = new AdminWelcomePageManageAccounts();
                q.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows "AdminWelcomePageOrders" window.
        /// </summary>
        private void ButtonClick_Orders(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageOrders();
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
        /// This method allows "Administrator"s to change price of products.
        /// if nothing is selected or price is set to 0 it shows a MessageBox.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method refreshes data, by closing window and reopening it.
        /// </summary>
        public void Update()
        {
            var _ = new AdminWelcomePageManageProducts();
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
