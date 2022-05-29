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
using Login;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Program
{
    public partial class LoginWindow : Window
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public LoginWindow()
        {
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .Select(q => q.Id)
                    .FirstOrDefault();
                if (userData == 0)
                {
                    db.UserLogins.Add(new UserLogin()
                    {
                        UserName = "Admin", Password = "Admin", Email = "example@gmail.com",
                        AccountType = "Administrator"
                    });
                    db.SaveChanges();
                }
            }
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                    var userNameExists = userNames.Contains(UserName);
                    var correctPassword = "";
                    if (userNameExists)
                    {
                        correctPassword = db.UserLogins
                            .Where(q => q.UserName == UserName)
                            .Select(q => q.Password)
                            .ToList()
                            .Last();
                    }
                    var passwordCheck = correctPassword == Password;
                    if (userNameExists && passwordCheck)
                    {
                        MessageBox.Show("Successfully logged in.");
                        var administrators = db.UserLogins
                            .Where(q => q.AccountType == "Administrator")
                            .Select(q => q.UserName)
                            .ToList();
                        if (administrators.Contains(UserName))
                        {
                            var welcomePage = new AdminWelcomePage();
                            welcomePage.Show();
                        }
                        else
                        {
                            var welcomePage = new WelcomePage();
                            welcomePage.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct Username and Password.\nIf you don't have account press register to create one.");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong" + nameof(exception));
                Close();
            }
        }
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            var registration = new Registration();
            registration.Show();
        }
    }
}
