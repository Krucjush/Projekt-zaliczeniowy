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
    /// Logika interakcji dla klasy ManageFirstName.xaml
    /// </summary>
    public partial class ManageFirstName : Window
    {
        public string NewFirstName { get; set; }

        public ManageFirstName()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.FirstName != null)
                {
                    TextBlockFirstNameExists.Text = "Currently provided First Name";
                    TextBlockFirstName.Text = userData.FirstName;
                }
            }
            DataContext = this;
        }

        private void Button_Click_Change(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (!string.IsNullOrEmpty(NewFirstName))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.FirstName = NewFirstName;
                        db.SaveChanges();
                        MessageBox.Show("First Name successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewFirstName))
                    {
                        MessageBox.Show("If you wan to remove your First Name, Press \"Remove\" button");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong. Error:\n" + nameof(e));
                Close();
            }
        }
        private void Button_Click_Remove(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    if (userData?.FirstName != null)
                    {
                        userData.FirstName = null;
                        MessageBox.Show("Successfully removed First Name.");
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("First Name is already empty.");
                        Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong. Error:\n" + nameof(e));
                Close();
            }
        }
    }
}
