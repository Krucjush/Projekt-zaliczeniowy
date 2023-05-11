using System;
using System.Data.Linq;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using Program;

namespace Login
{
    /// <summary>
    /// This is login class.
    /// It is used by "Administrator"s and "Customer"s to access their accounts.
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static string UserName { get; set; }
        public string Password { private get; set; }
        /// <summary>
        /// This constructor generates binding, and creates "Administrator" account in case of one not existing.
        /// </summary>
        public LoginWindow()
        {
            try
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
                            UserName = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("Admin"), Email = "example@gmail.com",
                            AccountType = "Administrator"
                        });
                        db.SaveChanges();
                    }
                }
                InitializeComponent();
                DataContext = this;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This class allows users to access their account, only if "UserName" and "Password" is correct.
        /// If "UserName" or "Password" are incorrect, or "Password" doesn't match "UserName" in database it shows a MessageBox.
        /// If user is an "Administrator" it shows them "AdminWelcomePage" window.
        /// If user is a "Customer" it shows them "WelcomePage" window.
        /// </summary>
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                var userNameExists = userNames.Contains(UserName);
                var hashedPassword = "";
                if (userNameExists)
                {
                    hashedPassword = db.UserLogins
                        .Where(q => q.UserName == UserName)
                        .Select(q => q.Password)
                        .ToList()
                        .Last();
                }
                var passwordCheck = BCrypt.Net.BCrypt.Verify(Password, hashedPassword);
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
                    MessageBox.Show("Please enter correct Username and Password.\nIf you don't have account press register to create one.\nIf you forgot password press the hyperlink below.");
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows "Registration" window.
        /// </summary>
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            try
            {
                var registration = new Registration();
                registration.Show();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows "RecoverPassword" window.
        /// </summary>
        private void Hyperlink_Click_Forgot_Password(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new RecoverPassword();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            Close();
        }
        /// <summary>
        /// This method updates the Password each time user input changes.
        /// </summary>
        private void UserPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        { 
	        ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
        }
	}
}