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
    /// Logika interakcji dla klasy ManageZipCode.xaml
    /// </summary>
    public partial class ManageZipCode : Window
    {
        public string NewZipCode { get; set; }
        public ManageZipCode()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.ZipCode != null)
                {
                    TextBlockZipCodeExists.Text = "Currently provided Zip Code";
                    TextBlockZipCode.Text = userData.ZipCode;
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
                    if (!string.IsNullOrEmpty(NewZipCode) && NewZipCode.Contains("-") && NewZipCode.Length == 6)
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.ZipCode = NewZipCode;
                        db.SaveChanges();
                        MessageBox.Show("Zip Code successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewZipCode))
                    {
                        MessageBox.Show("If you wan to remove your Zip Code, Press \"Remove\" button");
                    }
                    else
                    {
                        MessageBox.Show("Wrong Zip Code");
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
                    if (userData?.ZipCode != null)
                    {
                        userData.ZipCode = null;
                        MessageBox.Show("Successfully removed Zip Code.");
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Zip Code is already empty.");
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
