using System;
using System.Linq;
using System.Windows;
using Program;

namespace Login
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
                    .Where(q => q.AccountType == "Administrator")
                    .ToList();
                if (userData.Count == 0)
                {
                    db.UserLogins.Add(new UserLogin()
                    {
                        UserName = "Admin", Password = "Admin", Email = "example@gmail.com",
                        AccountType = "Administrator"
                    });
                    db.SaveChanges();
                    MessageBox.Show(
                        "Created Administrator account with\nUserName: Admin\nPassword Admin\nRemember to change User Name and Password!");
                }
            }
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
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
                        Close();
                    }
                    else
                    {
                        var welcomePage = new WelcomePage();
                        welcomePage.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter correct Username and Password.\nIf you don't have account press register to create one.");
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
