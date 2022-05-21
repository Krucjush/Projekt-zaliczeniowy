﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Program
{
    public partial class LoginWindow : Window
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            using (UsersContext db = new UsersContext())
            {
                var userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                bool userNameExists = userNames.Contains(UserName);
                string correctPassword = "";
                if (userNameExists)
                {
                    correctPassword = db.UserLogins
                       .Where(q => q.UserName == UserName)
                       .Select(q => q.Password)
                       .ToList()
                       .Last();
                }
                bool passwordCheck = correctPassword == Password;
                if (userNameExists && passwordCheck)
                {
                    MessageBox.Show("Successfully loged in.");
                    var welcomePage = new WelcomePage();
                    welcomePage.Show();
                }
                else
                {
                    MessageBox.Show("Please enter correct Username and Password.\nIf you dont have account press register to create one.");
                }
            }
        }
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            var registration = new Registration();
            registration.Show();
        }
    }
}
