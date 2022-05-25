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
    /// Logika interakcji dla klasy ManagePhoneNumber.xaml
    /// </summary>
    public partial class ManagePhoneNumber : Window
    {
        public int NewPhoneNumber { get; set; }
        public ManagePhoneNumber()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.PhoneNumber != 0)
                {
                    TextBlockPhoneNumberExists.Text = "Currently provided Phone Number";
                    TextBlockPhoneNumber.Text = userData.PhoneNumber.ToString();
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
                    if (NewPhoneNumber != 0 && NewPhoneNumber.ToString().Length == 9)
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.PhoneNumber = NewPhoneNumber;
                        db.SaveChanges();
                        MessageBox.Show("Phone Number successfully set.");
                        Close();
                    }
                    else if (NewPhoneNumber == 0)
                    {
                        MessageBox.Show("If you wan to remove your Phone Number, Press \"Remove\" button");
                    }
                    else
                    {
                        MessageBox.Show("Wrong Phone number");
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
                    if (userData?.PhoneNumber != 0)
                    {
                        userData.PhoneNumber = 0;
                        MessageBox.Show("Successfully removed Phone Number.");
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Phone Number is already empty.");
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
