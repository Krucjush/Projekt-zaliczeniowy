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
    /// Logika interakcji dla klasy ManageAge.xaml
    /// </summary>
    public partial class ManageAge : Window
    {
        public int NewAge { get; set; }

        public ManageAge()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.Age != 0)
                {
                    TextBlockAgeExists.Text = "Currently provided Age";
                    TextBlockAge.Text = userData.Age.ToString();
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
                    if (NewAge != 0)
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Age = NewAge;
                        db.SaveChanges();
                        MessageBox.Show("Age successfully set.");
                        Close();
                    }
                    else if (NewAge == 0)
                    {
                        MessageBox.Show("If you wan to remove your Age, Press \"Remove\" button");
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
                    if (userData?.Age != 0)
                    {
                        userData.Age = 0;
                        MessageBox.Show("Successfully removed Age.");
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Age is already empty.");
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
