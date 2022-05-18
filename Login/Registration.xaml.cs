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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=InternetStore;Integrated Security=True";
            using (UsersContext db = new UsersContext(connectionString))
            {
                List<string> userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                List<string> emails = db.UserLogins
                    .Select(q => q.Email)
                    .ToList();
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    List<string> emptyFields = new List<string>();
                    if (string.IsNullOrEmpty(UserName))
                    {
                        emptyFields.Add("UserName");
                    }
                    if (string.IsNullOrEmpty(Email))
                    {
                        emptyFields.Add("Email");
                    }
                    if (string.IsNullOrEmpty(Password))
                    {
                        emptyFields.Add("Password");
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
                else if (Email != ConfirmEmail)
                {
                    MessageBox.Show("Emails are not the same.");
                }
                else if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Passwords are not the same.");
                }
                else
                {
                    db.Add(new UserLogin { UserName = UserName, Password = Password, Email = Email, AccountType = "Customer"});
                    db.SaveChanges();
                    MessageBox.Show("Successfully registered.\nYou can login now.");
                    Close();
                }
            }
        }
    }
}