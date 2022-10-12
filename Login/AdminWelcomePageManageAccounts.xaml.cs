﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Program;

namespace Login
{
    /// <summary>
    /// This class is available only to "Administrator"s.
    /// It allows them to see all users data, delete users, and add new ones.
    /// </summary>
    public partial class AdminWelcomePageManageAccounts : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        /// <summary>
        /// This constructor generates binding, and fills "Accounts" DataGrid.
        /// </summary>
        public AdminWelcomePageManageAccounts()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var a = db.UserLogins
                    .ToList();
                Accounts.ItemsSource = a;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageStocks" window.
        /// </summary>
        private void ButtonClick_ManageStocks(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageStocks();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageExpenses" window.
        /// </summary>
        private void ButtonClick_ManageExpenses(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new AdminWelcomePageManageExpenses();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "AdminWelcomePageManageProducts" window.
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
        /// This method opens "AdminWelcomePageOrders" window.
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
        /// This method allows administrators to add new accounts and add them to a database.
        /// Each account must have a "UserName", "Password" and "Email".
        /// "UserName" and "Email" shouldn't repeat in database, yet "Administrator"s are able to bypass this.
        /// "Email" must be correct format.
        /// "AccountType" allows to add new administrators.
        /// "AccountType" allows only "Administrator" and "Customer".
        /// </summary>
        private void ButtonClick_AddAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                    var emails = db.UserLogins
                        .Select(q => q.Email)
                        .ToList();
                    if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    {
                        var emptyFields = new List<string>();
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
                        var emptyFieldsString = string.Join(" ", emptyFields);
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
                    else if (!IsValidEmail(Email))
                    {
                        MessageBox.Show("Wrong Email");
                    }
                    else if (!Password.Any(char.IsLower) || !Password.Any(char.IsUpper) || !Password.Any(char.IsNumber) || Password.Length < 8 || Password.ToLower().Contains(UserName.ToLower()))
                    {
                        MessageBox.Show("Password should follow the following rules:\nAt least one (lowercase and capital) letter is needed,\nAt least one number is needed,\nMust be at least 8 characters long,\nCannot be too similar to User Name.");
                        var messageBoxResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            db.UserLogins.Add(new UserLogin { UserName = UserName, Password = Password, Email = Email, AccountType = AccountType == "Administrator" ? "Administrator" : "Customer" });
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        db.UserLogins.Add(new UserLogin { UserName = UserName, Password = Password, Email = Email, AccountType = AccountType == "Administrator" ? "Administrator" : "Customer" });
                        db.SaveChanges();
                    }
                }
                Update();

            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens "ManageAccountAdmin" window, if row is selected.
        /// Otherwise it shows a MessageBox.
        /// </summary>
        private void ButtonClick_EditAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (UserLogin)Accounts.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var selectedAccount = db.UserLogins
                            .Where(t => t.Id == row.Id)
                            .ToList()
                            .LastOrDefault();
                        var _ = new ManageAccountAdmin(selectedAccount);

                        _.Show();
                    }
                    Close();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator" to remove accounts form database, if correct user is selected.
        /// If no user is selected it shows a MessageBox.
        /// If it would remove the last "Administrator" it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (UserLogin)Accounts.SelectedItem;
                if (row == null)
                {
                    MessageBox.Show("Item not selected");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var admins = db.UserLogins
                            .Where(q => q.AccountType == "Administrator")
                            .ToList();
                        if (admins.Count <= 1 && row.AccountType == "Administrator")
                        {
                            MessageBox.Show("You can't remove last Administrator Account");
                        }
                        else
                        {
                            var selectedAccount = db.UserLogins
                                .Where(t => t.Id == row.Id)
                                .ToList()
                                .LastOrDefault();
                            db.UserLogins.Remove(selectedAccount);
                            db.SaveChanges();
                        }
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
        /// This method refreshes data by closing window and reopening it.
        /// </summary>
        public void Update()
        {
            var _ = new AdminWelcomePageManageAccounts();
            _.Show();
            Close();
        }
        /// <summary>
        /// This method checks if email is of correct format, using regex.
        /// </summary>
        /// <param name="email">Email provided by "Administrator"</param>
        /// <returns>True if email is of correct format, otherwise false.</returns>
        private static bool IsValidEmail(string email)
        {
            const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|pl)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// This method display information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
