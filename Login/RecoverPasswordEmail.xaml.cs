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
    /// Logika interakcji dla klasy RecoverPasswordEmail.xaml
    /// </summary>
    public partial class RecoverPasswordEmail : Window
    {
        public string Code { get; set; }
        public string RightCode { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        private readonly Random _random = new();
        private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        public RecoverPasswordEmail(string rightCode, string userName)
        {
            RightCode = rightCode;
            UserName = userName;
            //since I'm not advanced enough to send emails the right code will just be provided on entering this page.
            MessageBox.Show("Code:\n" + RightCode);
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click_Finish(object sender, RoutedEventArgs e)
        {
            if (Code == RightCode)
            {
                do
                {
                    NewPassword = PasswordGenerator();
                } while (!NewPassword.Any(char.IsLower) || !NewPassword.Any(char.IsUpper) ||
                         !NewPassword.Any(char.IsNumber));
                MessageBox.Show("Your new password,\nYou can change it in manage account page:\n" + NewPassword);
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
                    var n = _random.Next(0, 9);
                    RightCode += n.ToString();
                }
                MessageBox.Show("Wrong code.\nNew code have been sent.\n" + RightCode);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }
        private string PasswordGenerator()
        {
            return new string(Enumerable.Repeat(Chars, 8).Select(q => q[_random.Next(q.Length)]).ToArray());
        }
    }
}
