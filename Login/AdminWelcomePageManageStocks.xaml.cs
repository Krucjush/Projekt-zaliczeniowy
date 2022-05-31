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
                    .Select(q => q)
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
            using (var db = new UsersContext())
            {
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
                            db.Stocks.Add(new Stock { ItemName = ItemName, DateCreated = DateTime.Now, Quantity = Quantity });
                            db.SaveChanges();
                            Close();
                            var q = new AdminWelcomePageManageStocks();
                            q.Show();
                        }

                        break;
                    }
                }
            }
        }

        private void ButtonClick_RemoveStocks(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var row = (Stock)Stocks.SelectedItem;
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
                    var q = new AdminWelcomePageManageStocks();
                    Close();
                    q.Show();
                }
            }
        }

        private void ButtonClick_EditStocks(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var row = (Stock)Stocks.SelectedItem;
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
                    if (ItemName == null && Quantity == 0)
                    {
                        MessageBox.Show("Item Name and Quantity not provided");
                    }
                    else if (ItemName == null)
                    {
                        MessageBox.Show("Item Name not provided");
                    }
                    else if (Quantity == 0)
                    {
                        MessageBox.Show("Quantity not provided");
                    }
                    else
                    {
                        selectedStock.ItemName = ItemName;
                        selectedStock.Quantity = Quantity;
                        selectedStock.DateModified = DateTime.Now;
                        db.SaveChanges();
                        Close();
                        var q = new AdminWelcomePageManageStocks();
                        q.Show();
                    }
                }
            }
        }
    }
}
