﻿using System;
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
    /// Logika interakcji dla klasy AdminWelcomePageManageAccounts.xaml
    /// </summary>
    public partial class AdminWelcomePageManageAccounts : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public AdminWelcomePageManageAccounts()
        {
            InitializeComponent();
            DataContext = this;
            using (var db = new UsersContext())
            {
                var a = db.UserLogins
                    .Select(q => q)
                    .ToList();
                Accounts.ItemsSource = a;
            }
        }

        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            Close();
            var _ = new AdminWelcomePageManageStocks();
            _.Show();
        }

        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            Close();
            var _ = new AdminWelcomePageManageExpenses();
            _.Show();
        }

        private void ButtonClick_AddAccount(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                var emails = db.UserLogins
                    .Select(q => q.Email)
                    .ToList();
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(AccountType))
                {
                    List<string> emptyFields = new List<string>();
                    if (string.IsNullOrEmpty(UserName))
                    {
                        emptyFields.Add("User Name");
                    }
                    if (string.IsNullOrEmpty(Email))
                    {
                        emptyFields.Add("Email");
                    }
                    if (string.IsNullOrEmpty(Password))
                    {
                        emptyFields.Add("Password");
                    }

                    if (string.IsNullOrEmpty(AccountType))
                    {
                        emptyFields.Add("Account Type");
                    }
                    string emptyFieldsString = string.Join(" ", emptyFields);
                    if (emptyFields.Count == 1)
                    {
                        MessageBox.Show(emptyFieldsString + " cannot be empty");
                    }
                    else
                        MessageBox.Show("this fields cannot be empty: " + emptyFieldsString);
                }
                else if (userNames.Contains(UserName))
                {
                    MessageBox.Show("User name is taken");
                }
                else if (emails.Contains(Email))
                {
                    MessageBox.Show("Email is taken");
                }
                else
                {
                    db.UserLogins.Add(new UserLogin { UserName = UserName, Password = Password, Email = Email, AccountType = AccountType });
                    db.SaveChanges();
                    Close();
                    var _ = new AdminWelcomePageManageAccounts();
                    _.Show();
                }
            }
        }

        private void ButtonClick_EditAccount(object sender, RoutedEventArgs e)
        {
            var selectedAccount = new UserLogin();
            using (var db = new UsersContext())
            {
                var row = (UserLogin)Accounts.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    selectedAccount = db.UserLogins
                        .Where(t => t.Id == row.Id)
                        .ToList()
                        .LastOrDefault();
                }
            }
            var _ = new ManageAccountAdmin(selectedAccount);
            _.Show();
        }

        private void ButtonClick_RemoveAccount(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var row = (UserLogin)Accounts.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    var selectedAccount = db.UserLogins
                        .Where(t => t.Id == row.Id)
                        .ToList()
                        .LastOrDefault();
                    db.UserLogins.Remove(selectedAccount);
                    db.SaveChanges();
                    Close();
                    var _ = new AdminWelcomePageManageAccounts();
                    _.Show();
                }
            }
        }
    }
}
