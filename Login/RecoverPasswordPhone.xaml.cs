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
    /// Interaction logic for RecoverPasswordPhone.xaml
    /// </summary>
    public partial class RecoverPasswordPhone : Window
    {
        public string Code { get; set; }
        public string RightCode { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        private readonly Random _random = new();
        private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";

        public RecoverPasswordPhone(string rightCode, string userName)
        {
            try
            {
                RightCode = rightCode;
                UserName = userName;
                //since I'm not advanced enough to send emails the right code will just be provided on entering this page.
                MessageBox.Show("Code:\n" + RightCode);
                InitializeComponent();
                DataContext = this;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        private void Button_Click_Finish(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Code == RightCode)
                {
                    do
                    {
                        NewPassword = PasswordGenerator();
                    } while (!NewPassword.Any(char.IsLower) || !NewPassword.Any(char.IsUpper) ||
                             !NewPassword.Any(char.IsNumber));

                    MessageBox.Show("Your new password:\n" + NewPassword + "\nYou can change it in manage account page.");
                    using (var db = new UsersContext())
                    {
                        var user = db.UserLogins
                            .FirstOrDefault(q => q.UserName == UserName);
                        user.Password = NewPassword;
                        db.SaveChanges();
                    }

                    var _ = new LoginWindow();
                    _.Show();
                    Close();
                }
                else
                {
                    RightCode = "";
                    for (var i = 0; i < 6; i++)
                    {
                        var t = _random.Next(0, 9);
                        RightCode += t.ToString();
                    }

                    MessageBox.Show("Wrong code.\nNew code have been sent.\n" + RightCode);
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

        private void Button_Click_Resend(object sender, RoutedEventArgs e)
        {
            try
            {
                RightCode = "";
                for (var i = 0; i < 6; i++)
                {
                    var t = _random.Next(0, 9);
                    RightCode += t.ToString();
                }

                MessageBox.Show("New code has been sent:\n" + RightCode);
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        private string PasswordGenerator()
        {
            return new string(Enumerable.Repeat(Chars, 8).Select(q => q[_random.Next(q.Length)]).ToArray());
        }
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
