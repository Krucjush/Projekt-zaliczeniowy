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
    /// This class is available only for "Administrator"s.
    /// It allows them to view, edit quantity and remove all stocks.
    /// They cannot be added directly, without them being noted in expenses.
    /// </summary>
    public partial class AdminWelcomePageManageStocks : Window
    {
        public long Quantity { get; set; }
        /// <summary>
        /// This constructor generates binding and fills "Stocks" DataGrid.
        /// </summary>
        public AdminWelcomePageManageStocks()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var s = db.Stocks
                    .Select(q => new StockTable { StockId = q.StockId, ItemName = q.Products.Where(p => p.StockId == q.StockId).Select(p => p.ProductName).FirstOrDefault(), Quantity = q.Quantity, DateCreated = q.DateCreated, DateModified = q.DateModified })
                    .ToList();
                Stocks.ItemsSource = s;
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
        /// This method shows "AdminWelcomePageManageProducts" window.
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
        /// This method allows "Administrator"s to remove stock from database.
        /// If nothing is selected it shows a MessageBox.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to edit stocks quantity.
        /// If nothing is selected, no quantity is given, or quantity is equal to 0, it shows a MessageBox.
        /// </summary>
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
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method updates data, by closing and reopening window.
        /// </summary>
        private void Update()
        {
            var _ = new AdminWelcomePageManageStocks();
            _.Show();
            Close();
        }
        /// <summary>
        /// This method shows user  information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
