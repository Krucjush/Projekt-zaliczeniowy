using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Login
{
    public partial class MainWindow : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Internet Store;Integrated Security=True";
            using (UsersContext db = new UsersContext(connectionString))
            {
                
                List<string> userNames = db.UserLogins
                    .Select(q => q.UserNames)
                    .ToList();
                bool userNameExists = userNames.Contains(UserName);
                List<string> passwords = db.UserLogins
                    .Select(q => q.Passwords)
                    .ToList();
                bool passwordExists = passwords.Contains(Password);
                if (userNameExists && passwordExists)
                {
                    MessageBox.Show("Successfully loged in.");
                    WelcomePage settingsForm = new WelcomePage();
                    settingsForm.Show();
                }
                else
                {
                    MessageBox.Show("Please enter correct Username and Password.");
                }
            }
        }
    }
}
