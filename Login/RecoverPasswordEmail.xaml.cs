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
        private string Code { get; set; }
        private string RightCode { get; set; }
        private string UserName { get; set; }
        private string NewPassword { get; set; }
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
                NewPassword = new string(Enumerable.Repeat(Chars, 8)
                    .Select(q => q[_random.Next(q.Length)])
                    .ToArray());
                MessageBox.Show(NewPassword);
                //using (var db = new UsersContext())
                //{
                //    var user = db.UserLogins
                //        .FirstOrDefault(q => q.UserName == UserName);
                //    //user.Password = 
                //}
            }
            else
            {
                RightCode = "";
                for (var i = 0; i < 7; i++)
                {
                    var n = _random.Next(0, 10);
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
    }
}
