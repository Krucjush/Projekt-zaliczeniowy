﻿using System;
using System.Collections.Generic;
using System.Data;
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
        public string ExpensesName { get; set; }
        public long Amount { get; set; }
        public long Cost { get; set; }
        public AdminWelcomePageManageExpenses()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var e = db.Expenses
                    .ToList();
                Expenses.ItemsSource = e;
            }

            DataContext = this;
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
        private void ButtonClick_AddExpenses(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            switch (ExpensesName)
            {
                case null when Amount == 0:
                    MessageBox.Show("You cannot add an empty field.");
                    break;
                case null:
                    MessageBox.Show("Expenses require name.");
                    break;
                default:
                {
                    if (Amount == 0)
                    {
                        MessageBox.Show("Amount is required");
                    }
                    else
                    {
                        db.Expenses.Add(new Expense { ExpensesName = ExpensesName, Date = DateTime.Now, Amount = Amount, Cost = Cost });
                        db.SaveChanges();
                        Update();
                    }

                    break;
                }
            }
        }
        private void ButtonClick_AddToStocks(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Expense)Expenses.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected.");
            }
            else
            {
                var selectedExpense = db.Expenses
                    .Where(t => t.ExpenseId == row.ExpenseId)
                    .ToList()
                    .LastOrDefault();
                db.Stocks.Add(new Stock
                {
                    Quantity = selectedExpense.Amount, DateCreated = DateTime.Now
                });
                db.SaveChanges();
                Update();
            }
        }

        private void ButtonClick_RemoveExpenses(object sender, RoutedEventArgs e)
        {
            using var db = new UsersContext();
            var row = (Expense)Expenses.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected.");
            }
            else
            {
                var selectedExpense = db.Expenses
                    .Where(t => t.ExpenseId == row.ExpenseId)
                    .ToList()
                    .LastOrDefault();
                db.Expenses.Remove(selectedExpense);
                db.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var _ = new AdminWelcomePageManageExpenses();
            _.Show();
            Close();
        }
    }
}
