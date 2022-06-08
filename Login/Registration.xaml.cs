using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userNames = db.UserLogins
                        .Select(q => q.UserName)
                        .ToList();
                    var emails = db.UserLogins
                        .Select(q => q.Email)
                        .ToList();
                    if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    {
                        var emptyFields = new List<string>();
                        if (string.IsNullOrEmpty(UserName))
                        {
                            emptyFields.Add("UserName");
                        }
                        if (string.IsNullOrEmpty(Email))
                        {
                            emptyFields.Add("Email");
                        }
                        if (string.IsNullOrEmpty(Password))
                        {
                            emptyFields.Add("Password");
                        }
                        var emptyFieldsString = string.Join(" ", emptyFields);
                        if (emptyFields.Count == 1)
                        {
                            MessageBox.Show(emptyFieldsString + " cannot be empty.");
                        }
                        else
                            MessageBox.Show("this fields cannot be empty: " + emptyFieldsString + ".");
                    }
                    else if (userNames.Contains(UserName))
                    {
                        MessageBox.Show("User name is taken.");
                    }
                    else if (emails.Contains(Email))
                    {
                        MessageBox.Show("Email address is taken.");
                    }
                    else if (Email != ConfirmEmail)
                    {
                        MessageBox.Show("Emails are not the same.");
                    }
                    else if (Password != ConfirmPassword)
                    {
                        MessageBox.Show("Passwords are not the same.");
                    }
                    else if (Password.Contains(UserName))
                    {
                        MessageBox.Show("Password is too similar to User Name.");
                    }
                    else
                    {
                        db.UserLogins.Add(new UserLogin { UserName = UserName, Password = Password, Email = Email, AccountType = "Customer" });
                        db.SaveChanges();
                        MessageBox.Show("Successfully registered.\nYou can login now.");
                        Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong." + nameof(e));
            }
        }
    }
}
