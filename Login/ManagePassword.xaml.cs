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
using Program;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy ManagePassword.xaml
    /// </summary>
    public partial class ManagePassword : Window
    {
        public string NewPassword { get; set; }

        public ManagePassword()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBlockPassword.Text = userData.Password;
            }
            DataContext = this;
        }

        private void Button_Click_Change(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                    var userNameDoesNotExist = userNames.Contains(NewPassword);
                    if (!userNameDoesNotExist && !string.IsNullOrEmpty(NewPassword))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.UserName = NewPassword;
                        db.SaveChanges();
                        LoginWindow.UserName = NewPassword;
                        MessageBox.Show("Password successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewPassword))
                    {
                        MessageBox.Show("Password cannot be null.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong." + nameof(e));
                Close();
            }
        }
    }
}
