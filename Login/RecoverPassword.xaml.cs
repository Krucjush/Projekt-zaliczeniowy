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

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : Window
    {
        public string UserName { get; set; }
        public RecoverPassword()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                if (string.IsNullOrEmpty(UserName))
                {
                    MessageBox.Show("Provide User Name.");
                }
                else if (!userNames.Contains(UserName))
                {
                    MessageBox.Show("Wrong User Name.");
                }
                else
                {
                    var _ = new RecoverPassword2(UserName);
                    _.Show();
                    Close();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
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
        private void Error(Exception exception)
        {
            MessageBox.Show("something went wrong\n" + exception);
            Close();
        }
    }
}
