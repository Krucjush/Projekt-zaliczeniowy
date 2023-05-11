using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Login
{
    /// <summary>
    /// This class allows users to create accounts.
    /// </summary>
    public partial class Registration : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        /// <summary>
        /// This constructor generates binding.
        /// </summary>
        public Registration()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method adds created account to database.
        /// For account to be created, following conditions must be met:
        /// UserName, Email and password mustn't be empty,
        /// UserName and Email cannot already exist in database,
        /// Email must match the one given in Confirm Email,
        /// Password must match te one given in Confirm Password,
        /// Password must follow the right format:
        ///     At least one lowercase and capital letter,
        ///     At least one number,
        ///     At least 8 characters long,
        ///     Mustn't contain UserName in it;
        /// Email mus be of correct format,
        /// </summary>
        private void Button_Click_Register(object sender, RoutedEventArgs eventArgs)
        {
            try
            {
                using var db = new UsersContext();
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
                else if (!Password.Any(char.IsLower) || !Password.Any(char.IsUpper) || !Password.Any(char.IsNumber) || Password.Length < 8 || Password.ToLower().Contains(UserName.ToLower()))
                {
                    MessageBox.Show("Password must follow the following rules:\nAt least one (lowercase and capital) letter is needed,\nAt least one number is needed,\nMust be at least 8 characters long,\nCannot be too similar to User Name.");
                }
                else if (!IsValid(Email))
                {
                    MessageBox.Show("Wrong email");
                }
                else
                {
                    db.UserLogins.Add(new UserLogin { UserName = UserName, Password = BCrypt.Net.BCrypt.HashPassword(Password), Email = Email, AccountType = "Customer" });
                    db.SaveChanges();
                    MessageBox.Show("Successfully registered.\nYou can login now.");
                    Close();
                }
            }
            catch(Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method checks if Email is of correct format using regex.
        /// </summary>
        /// <param name="email">Email given by user</param>
        /// <returns>True if email matches the criteria, otherwise false</returns>
        private static bool IsValid(string email)
        {
            const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|pl)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private static void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
        }
    }
}
