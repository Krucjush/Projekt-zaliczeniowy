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
    /// Logika interakcji dla klasy ManageEmail.xaml
    /// </summary>
    public partial class ManageEmail : Window
    {
        public string NewEmail { get; set; }
        public ManageEmail()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBlockEmail.Text = userData.Email;
            }
            DataContext = this;
        }
        private void Button_Click_Change(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (!string.IsNullOrEmpty(NewEmail) && NewEmail.Contains("@") && NewEmail.Contains("."))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Email = NewEmail;
                        db.SaveChanges();
                        MessageBox.Show("Email successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewEmail))
                    {
                        MessageBox.Show("Email cannot be null.");
                    }
                    else if(!NewEmail.Contains("@") || !NewEmail.Contains("."))
                    {
                        MessageBox.Show("Wrong Email.");
                    }
                    else
                    {
                        MessageBox.Show("Email is taken.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong. Error: " + nameof(e));
                Close();
            }
        }
    }
}
