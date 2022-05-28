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
    /// Logika interakcji dla klasy ManageAddress.xaml
    /// </summary>
    public partial class ManageAddress : Window
    {
        public string NewAddress { get; set; }

        public ManageAddress()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.Address != null)
                {
                    TextBlockAddressExists.Text = "Currently provided Address";
                    TextBlockAddress.Text = userData.Address;
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
                    if (!string.IsNullOrEmpty(NewAddress))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Address = NewAddress;
                        db.SaveChanges();
                        MessageBox.Show("Address successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewAddress))
                    {
                        MessageBox.Show("If you wan to remove your Address, Press \"Remove\" button");
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
                    if (userData?.Address != null)
                    {
                        userData.Address = null;
                        MessageBox.Show("Successfully removed Address.");
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Address is already empty.");
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
