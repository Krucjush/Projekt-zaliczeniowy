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
        public string ItemName { get; set; }
        public long Quantity { get; set; }
        public AdminWelcomePageManageStocks()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var s = db.Stocks
                    .Select(q => new StockTable {StockId = q.StockId, Quantity = q.Quantity, DateCreated = q.DateCreated, DateModified = q.DateModified})
                    .ToList();
                Stocks.ItemsSource = s;
            }
            DataContext = this;
        }

        private void Button_Click_Manage_Expenses(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }

        private void Button_Click_Manage_Accounts(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageAccounts();
            q.Show();
            Close();
        }

        private void ButtonClick_AddStocks(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            switch (ItemName)
            {
                case null when Quantity == 0:
                    MessageBox.Show("You cannot add an empty field.");
                    break;
                case null:
                    MessageBox.Show("Expenses require name.");
                    break;
                default:
                {
                    if (Quantity == 0)
                    {
                        MessageBox.Show("Amount is required");
                    }
                    else
                    {
                        db.Stocks.Add(new Stock { DateCreated = DateTime.Now, Quantity = Quantity });
                        db.SaveChanges();
                        Update();
                    }
                    break;
                }
            }
        }

        private void ButtonClick_RemoveStocks(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (StockTable)Stocks.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedStock = db.Stocks
                    .Where(t => t.StockId == row.StockId)
                    .ToList()
                    .LastOrDefault();
                db.Stocks.Remove(selectedStock);
                db.SaveChanges();
                Update();
            }
        }

        private void ButtonClick_EditStocks(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (StockTable)Stocks.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                var selectedStock = db.Stocks
                    .Where(t => t.StockId == row.StockId)
                    .ToList()
                    .LastOrDefault();
                switch (ItemName)
                {
                    case null when Quantity == 0:
                        MessageBox.Show("No changes were made");
                        break;
                    case null:
                    {
                        selectedStock.Quantity = Quantity;
                        selectedStock.DateModified = DateTime.Now;
                        db.SaveChanges();
                        Update();
                            break;
                    }
                    default:
                    {
                        if (Quantity == 0)
                        {
                            selectedStock.ItemName = ItemName;
                            selectedStock.DateModified = DateTime.Now;
                            db.SaveChanges();
                            Update();
                        }
                        else
                        {
                            selectedStock.ItemName = ItemName;
                            selectedStock.Quantity = Quantity;
                            selectedStock.DateModified = DateTime.Now;
                            db.SaveChanges();
                            Update();
                        }

                        break;
                    }
                }
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
