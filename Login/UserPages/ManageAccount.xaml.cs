using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Login
{
    /// <summary>
    /// This class is available only for "Customer"s.
    /// It allows them to view, edit and remove their information in database.
    /// </summary>
    public partial class ManageAccount : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public bool DoClose { get; set; } = true;
        /// <summary>
        /// This constructor generates binding.
        /// </summary>
        public ManageAccount()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBoxUserName.Text = userData.UserName;
                TextBoxEmail.Text = userData.Email;
                TextBoxFirstName.Text = userData?.FirstName ?? "Not provided";
                TextBoxLastName.Text = userData?.LastName ?? "Not provided";
                TextBoxAge.Text = userData?.Age ?? "Not provided";
                TextBoxPhoneNumber.Text = userData?.PhoneNumber ?? "Not provided";
                TextBoxAddress.Text = userData?.Address ?? "Not provided";
                TextBoxZipCode.Text = userData?.ZipCode ?? "Not provided";
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to change their "UserName".
        /// If input is empty or already exists in database, it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetUserName(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
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
                    Update();
                }

            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to change their "Password".
        /// If input is empty or it doesn't follow the rules, it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetPassword(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password cannot be empty");
                }
                else if (!Password.Any(char.IsLower) || !Password.Any(char.IsUpper) || !Password.Any(char.IsNumber) || Password.Length < 8 || Password.ToLower().Contains(UserName.ToLower()))
                {
                    MessageBox.Show("Password must follow the following rules:\nAt least one (lowercase and capital) letter is needed,\nAt least one number is needed,\nMust be at least 8 characters long,\nCannot be too similar to User Name.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to change their "Email".
        /// If input is empty, or it doesn't follow the rules, it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    MessageBox.Show("Email cannot be empty");
                }
                else if (!IsValidEmail(Email))
                {
                    MessageBox.Show("Wrong Email");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Email = Email;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "FirstName".
        /// If input is empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetFirstName(object sender, RoutedEventArgs e)
        {
            try
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
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "FirstName" from database.
        /// If "UserName" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveFirstName(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
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
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "LastName".
        /// If input is empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetLastName(object sender, RoutedEventArgs e)
        {
            try
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
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "LastName" from database.
        /// If "LastName" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveLastName(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
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
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "Age".
        /// If input is empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetAge(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Age))
                {
                    MessageBox.Show("You can't set empty Age, if you want to remove Age, press \"Remove\" button");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Age = Age;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "Age" from database.
        /// If "Age" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveAge(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.Age == null)
                {
                    MessageBox.Show("Age is already empty");
                }
                else
                {
                    userData.Age = null;
                    db.SaveChanges();
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "PhoneNumber".
        /// If input is empty, or wrong format it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetPhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                var isCorrect = PhoneNumber.Any(t => !char.IsLetter(t));
                if (PhoneNumber.Length == 0)
                {
                    MessageBox.Show("You can't set empty Phone Number, if you want to remove Phone Number, press \"Remove\" button.");
                }
                else if(PhoneNumber.Length != 9 || isCorrect)
                {
                    MessageBox.Show("Wrong Phone Number.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.PhoneNumber = PhoneNumber;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "PhoneNumber" from database.
        /// If "PhoneNumber" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemovePhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.PhoneNumber == null)
                {
                    MessageBox.Show("Phone Number is already empty");
                }
                else
                {
                    userData.PhoneNumber = null;
                    db.SaveChanges();
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "Address".
        /// If input is empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetAddress(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Address))
                {
                    MessageBox.Show("You can't set empty Address, if you want to remove Address, press \"Remove\" button.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Address = Address;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "Address" from database.
        /// If "Address" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveAddress(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.Address == null)
                {
                    MessageBox.Show("Address is already empty");
                }
                else
                {
                    userData.Address = null;
                    db.SaveChanges();
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to set their "ZipCode".
        /// If ZipCode is empty or is of wrong format it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetZipCode(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ZipCode))
                {
                    MessageBox.Show("You can't set empty Zip Code, if you want to remove Zip Code, press \"Remove\" button.");
                }
                else if (!IsValidZipCode(ZipCode))
                {
                    MessageBox.Show("Wrong Zip Code");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.ZipCode = ZipCode;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Customer"s to remove their "ZipCode" from database.
        /// If "ZipCode" is already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveZipCode(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                if (userData?.ZipCode == null)
                {
                    MessageBox.Show("Zip Code is already empty");
                }
                else
                {
                    userData.ZipCode = null;
                    db.SaveChanges();
                    Update();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method updates data, by closing and reopening window.
        /// </summary>
        private void Update()
        {
            var _ = new ManageAccount();
            _.Show();
            DoClose = false;
            Close();
        }
        /// <summary>
        /// This method shows "WelcomePage" if "Customer" actually intends to close the window.
        /// </summary>
        private void ManageAccount_Closing(object sender, CancelEventArgs e)
        {
            if (!DoClose) return;
            var _ = new WelcomePage();
            _.Show();
        }
        /// <summary>
        /// This method is checking if "Email" is of correct format using regex.
        /// </summary>
        /// <param name="email">Email given by customer.</param>
        /// <returns>True if email is of correct format, otherwise false.</returns>
        private static bool IsValidEmail(string email)
        {
            const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|pl)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// This method is checking if "ZipCode" is of correct format using regex.
        /// </summary>
        /// <param name="zipCode">Zip code given by customer.</param>
        /// <returns>True if zip code is of correct format, otherwise false.</returns>
        private static bool IsValidZipCode(string zipCode)
        {
            const string regex = @"^[0-9]{2}-[0-9]{3}$";
            return Regex.IsMatch(zipCode, regex);
        }
        /// <summary>
        /// This method shows user information of error.
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("Something went wrong\n" + exception);
            DoClose = false;
            Close();
        }
    }
}
