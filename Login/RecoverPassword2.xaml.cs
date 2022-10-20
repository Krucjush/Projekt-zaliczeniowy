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

namespace Login
{
    /// <summary>
    /// This class is the second form for recovering password.
    /// Accessible by each user.
    /// </summary>
    public partial class RecoverPassword2 : Window
    {
        public string UserName { get; set; }
        public string RightCode { get; set; }
        private readonly Random _random = new();
        /// <summary>
        /// This constructor generates binding, and generates random code for recovery.
        /// </summary>
        /// <param name="userName">UserName of interested user.</param>
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
        /// <summary>
        /// This method opens next password recovery form.
        /// If Email is selected as recovery way it opens RecoverPasswordEmail form.
        /// If Phone is selected as recovery way it opens RecoverPasswordPhone form.
        /// If I don't have access to any above is selected as recovery way it shows a MessageBox, and opens LoginWindow.
        /// </summary>
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
                    using (var db = new UsersContext())
                    {
                        var user = db.UserLogins
                            .FirstOrDefault(x => x.UserName == UserName);
                        if (user.PhoneNumber == null)
                        {
                            MessageBox.Show("You didn't provide phone number.");
                            return;
                        }
                    }
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
        /// <summary>
        /// This method opens previous form for password recovery, RecoverPassword.
        /// </summary>
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
        /// <summary>
        /// This method opens LoginWindow.
        /// </summary>
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
        /// <summary>
        /// This method shows information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
