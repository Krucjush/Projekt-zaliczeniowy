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
    /// Logika interakcji dla klasy AdminWelcomePage.xaml
    /// </summary>
    public partial class AdminWelcomePage : Window
    {
        public AdminWelcomePage()
        {
            InitializeComponent();
        }
        private void Button_Click_Manage_Expenses(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageExpenses();
            q.Show();
            Close();
        }
        private void Button_Click_Manage_Stocks(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageStocks();
            q.Show();
            Close();
        }
        private void Button_Click_Manage_Accounts(object sender, RoutedEventArgs e)
        {
            var q = new AdminWelcomePageManageAccounts();
            q.Show();
            Close();
        }
    }
}