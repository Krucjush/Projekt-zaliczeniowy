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
using Program;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWelcomePageManageStocks.xaml
    /// </summary>
    public partial class AdminWelcomePageManageStocks : Window
    {
        public long Quantity { get; set; }
        public AdminWelcomePageManageStocks()
        {
            try
            {
                InitializeComponent();
                using (var db = new UsersContext())
                {
                    var s = db.Stocks
                        .Select(q => new StockTable { StockId = q.StockId, ItemName = q.Products.Where(p => p.StockId == q.StockId).Select(p => p.ProductName).FirstOrDefault(), Quantity = q.Quantity, DateCreated = q.DateCreated, DateModified = q.DateModified })
                        .ToList();
                    Stocks.ItemsSource = s;
                }
                DataContext = this;
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

        private void ButtonClick_ManageAccounts(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageAccounts();
            q.Show();
            Close();
        }
        private void ButtonClick_ManageProducts(object sender, RoutedEventArgs e)
        {
            var _ = new AdminWelcomePageManageProducts();
            _.Show();
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
        private void ButtonClick_RemoveStocks(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (StockTable)Stocks.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var selectedStock = db.Stocks
                            .Where(t => t.StockId == row.StockId)
                            .ToList()
                            .LastOrDefault();
                        db.Stocks.Remove(selectedStock);
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

        private void ButtonClick_EditStocks(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (StockTable)Stocks.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    
                    if (Quantity == 0)
                    {
                        MessageBox.Show("No changes were made.");
                    }
                    else
                    {
                        using (var db = new UsersContext())
                        {
                            var selectedStock = db.Stocks
                                .Where(t => t.StockId == row.StockId)
                                .ToList()
                                .LastOrDefault();
                            selectedStock.Quantity = Quantity;
                            selectedStock.DateModified = DateTime.Now;
                        }
                        Update();
                    }
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
            var _ = new AdminWelcomePageManageStocks();
            _.Show();
            Close();
        }
    }
}
