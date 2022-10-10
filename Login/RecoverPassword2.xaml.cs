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
    /// Logika interakcji dla klasy RecoverPassword2.xaml
    /// </summary>
    public partial class RecoverPassword2 : Window
    {
        public string UserName { get; set; }
        public string RightCode { get; set; }
        private readonly Random _random = new();
        public RecoverPassword2(string userName)
        {
            try
            {
                UserName = userName;
                InitializeComponent();
                for (var i = 0; i < 6; i++)
                {
                    var t = _random.Next(0, 9);
                    RightCode += t.ToString();
                }
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
                if ((bool)EmailRb.IsChecked)
                {
                    var _ = new RecoverPasswordEmail(RightCode, UserName);
                    _.Show();
                    Close();
                }
                else if ((bool)PhoneRb.IsChecked)
                {
                    var _ = new RecoverPasswordPhone(RightCode, UserName);
                    _.Show();
                    Close();
                }
                else if ((bool)NoneRb.IsChecked)
                {
                    MessageBox.Show("We cannot confirm it's really you.");
                    var _ = new LoginWindow();
                    _.Show();
                    Close();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new RecoverPassword();
                _.Show();
                Close();
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
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
