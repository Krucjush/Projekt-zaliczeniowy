using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IdentityModel.Selectors;
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
    /// This is one of the final paths for password recovery.
    /// This one is "using" Email to "confirm" it's really the person that tries to recover password.
    /// </summary>
    public partial class RecoverPasswordEmail : Window
    {
        public string Code { get; set; }
        public string RightCode { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        private readonly Random _random = new();
        private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        /// <summary>
        /// This constructor generates binding, and shows user the right code on starting this window.
        /// </summary>
        /// <param name="rightCode">Correct code</param>
        /// <param name="userName">UserName of interested user</param>
        public RecoverPasswordEmail(string rightCode, string userName)
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
        /// <summary>
        /// This method resets user password if the code given by user matches the one generated and "sent" to user.
        /// If the code given by user is wrong it shows a MessageBox, generates new code, and shows it to user on next MessageBox.
        /// If the code given by user is correct, it generates new password matching the criteria for correct password, and shows it to user in a MessageBox.
        /// </summary>
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
                        user.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
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
        /// This method generates new code, and shows is on a MessageBox.
        /// </summary>
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
        /// <summary>
        /// This method is a password generator.
        /// </summary>
        /// <returns>string of 8 random characters from Chars string</returns>
        private string PasswordGenerator()
        {
            return new string(Enumerable.Repeat(Chars, 8).Select(q => q[_random.Next(q.Length)]).ToArray());
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
    }
}
