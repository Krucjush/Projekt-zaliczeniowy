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
    /// Logika interakcji dla klasy AdminWelcomePageManageExpenses.xaml
    /// </summary>
    public partial class AdminWelcomePageManageExpenses : Window
    {
        public AdminWelcomePageManageExpenses()
        {
            InitializeComponent();
            TextBoxExpensesName.Text = "Expenses Name";
            TextBoxDate.Text = "Date";
            TextBoxAmount.Text = "Cost";
            using (var db = new UsersContext())
            {
                var e = db.Expenses
                    .Select(q => q)
                    .ToList();
                Expenses.ItemsSource = e;
            }
        }

        private void TextBoxExpensesName_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxExpensesName.Text == "Expenses Name")
            {
                TextBoxExpensesName.Text = "";
            }
        }

        private void TextBoxExpensesName_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxExpensesName.Text == "")
            {
                TextBoxExpensesName.Text = "Expenses Name";
            }
        }
        private void TextBoxDate_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxDate.Text == "Date")
            {
                TextBoxDate.Text = "";
            }
        }
        private void TextBoxDate_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxDate.Text == "")
            {
                TextBoxDate.Text = "Date";
            }
        }
        private void TextBoxAmount_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxAmount.Text == "Cost")
            {
                TextBoxAmount.Text = "";
            }
        }
        private void TextBoxAmount_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextBoxAmount.Text == "")
            {
                TextBoxAmount.Text = "Cost";
            }
        }
        
        private void Button_Click_Manage_Stocks(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }

        private void Button_Click_Manage_Accounts(object sender, RoutedEventArgs e)
        {
        }
        
    }
}
