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
using Login;

namespace Program
{
    /// <summary>
    /// Logika interakcji dla klasy ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public ManageAccount()
        {
            InitializeComponent();
            DataContext = this;
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBoxUserName.Text = userData.UserName;
                TextBoxPassword.Text = userData.Password;
                TextBoxEmail.Text = userData.Email;
                TextBoxFirstName.Text = userData?.FirstName ?? "Not provided";
                TextBoxLastName.Text = userData?.LastName ?? "Not provided";
                TextBoxAge.Text = userData?.Age != 0 ? userData.Age.ToString() : "Not provided";
                TextBoxPhoneNumber.Text = userData?.PhoneNumber != 0 ? userData.PhoneNumber.ToString() : "Not provided";
                TextBoxAddress.Text = userData?.Address ?? "Not provided";
                TextBoxZipCode.Text = userData?.ZipCode ?? "Not provided";
            }
        }

        private void ButtonClick_SetUserName(object sender, RoutedEventArgs e)
        {
            {
                using (var db = new UsersContext())
                {
                    var userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                    if (string.IsNullOrEmpty(UserName))
                    {
                        MessageBox.Show("User Name cannot be empty.");
                    }
                    else if (userNames.Contains(UserName))
                    {
                        MessageBox.Show("This User Name is taken.");
                    }
                    else
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.UserName = UserName;
                        MessageBox.Show("User Name successfully set.");
                        Close();
                        var _ = new ManageAccount();
                        _.Show();
                    }
                }
            }
        }

        private void ButtonClick_SetPassword(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password cannot be empty");
                }
                else
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    userData.Password = Password;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_SetEmail(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                if (string.IsNullOrEmpty(Email))
                {
                    MessageBox.Show("Email cannot be empty");
                }
                else if (!Email.Contains("@") && !Email.Contains("."))
                {
                    MessageBox.Show("Wrong Email");
                }
                else
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    userData.Email = Email;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_SetFirstName(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("You can't set empty First Name, if you want to remove Fist Name, press \"Remove\" button");
            }
            else
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    userData.FirstName = FirstName;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_RemoveFirstName(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.FirstName == null)
                {
                    MessageBox.Show("First Name is already empty");
                }
                else
                {
                    userData.FirstName = null;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_SetLastName(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("You can't set empty Last Name, if you want to remove Last Name, press \"Remove\" button");
            }
            else
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    userData.LastName = LastName;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_RemoveLastName(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.LastName == null)
                {
                    MessageBox.Show("Last Name is already empty");
                }
                else
                {
                    userData.LastName = null;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_SetAge(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                if (Age == 0)
                {
                    MessageBox.Show("You can't set empty Age, if you want to remove Age, press \"Remove\" button");
                }
                else
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                    userData.Age = Age;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }

        private void ButtonClick_RemoveAge(object sender, RoutedEventArgs e)
        {
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.Age == 0)
                {
                    MessageBox.Show("Age is already empty");
                }
                else
                {
                    userData.Age = 0;
                    db.SaveChanges();
                    Close();
                    var _ = new ManageAccount();
                    _.Show();
                }
            }
        }
    }
}
