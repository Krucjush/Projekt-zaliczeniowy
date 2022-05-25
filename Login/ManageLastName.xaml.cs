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
    /// Logika interakcji dla klasy ManageLastName.xaml
    /// </summary>
    public partial class ManageLastName : Window
    {
        public string NewLastName { get; set; }
        public ManageLastName()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.LastName != null)
                {
                    TextBlockLastNameExists.Text = "Currently provided Last Name";
                    TextBlockLastName.Text = userData.LastName;
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
                    if (!string.IsNullOrEmpty(NewLastName))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.LastName = NewLastName;
                        db.SaveChanges();
                        MessageBox.Show("Last Name successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewLastName))
                    {
                        MessageBox.Show("If you wan to remove your Last Name, Press \"Remove\" button");
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
                    if (userData?.LastName != null)
                    {
                        userData.LastName = null;
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
