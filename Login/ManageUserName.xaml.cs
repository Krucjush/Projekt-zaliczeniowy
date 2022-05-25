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
    /// Logika interakcji dla klasy ChangeData.xaml
    /// </summary>
    public partial class ManageUserName : Window
    {
        public string NewUserName { get; set; }
        public ManageUserName()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBlockUserName.Text = userData.UserName;
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
                    var userNameDoesNotExist = userNames.Contains(NewUserName);
                    if (!userNameDoesNotExist && !string.IsNullOrEmpty(NewUserName))
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.UserName = NewUserName;
                        db.SaveChanges();
                        LoginWindow.UserName = NewUserName;
                        MessageBox.Show("User Name successfully changed.");
                        Close();
                    }
                    else if (string.IsNullOrEmpty(NewUserName))
                    {
                        MessageBox.Show("User Name cannot be null.");
                    }
                    else
                    {
                        MessageBox.Show("User Name is taken.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong. Error: " + nameof(e));
            }
        }
    }
}
