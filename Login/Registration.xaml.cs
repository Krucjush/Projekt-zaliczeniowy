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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=InternetStore;Integrated Security=True";
            using (UsersContext db = new UsersContext(connectionString))
            {
                List<string> userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                bool userNameDoesntExist = !userNames.Contains(UserName);
                bool passwordsAreTheSame = Password == ConfirmPassword;
                if (UserName == null)
                {
                    MessageBox.Show("User name cannot be empty.");
                }
                else if (Password == null)
                    MessageBox.Show("Password cannot be empty.");
                else if (!passwordsAreTheSame)
                {
                    MessageBox.Show("Passwords are not the same.");
                }
                else if (!userNameDoesntExist)
                {
                    MessageBox.Show("User name is taken.");
                }
                else
                {
                    db.Add(new UserLogin { UserName = UserName, Password = Password });
                    db.SaveChanges();
                    MessageBox.Show("Successfully registered.\nYou can login now.");
                    Close();
                }
            }
        }
    }
}
