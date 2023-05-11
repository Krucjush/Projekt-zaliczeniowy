using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// This class is available only for "Administrator"s.
    /// It allows them to view the data about the selected account, and edit it.
    /// </summary>
    public partial class ManageAccountAdmin : Window
    {
        public UserLogin SelectedAccount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string AccountType { get; set; }
        public bool IsSelected { get; set; }
        public bool DoClose { get; set; } = true;
        /// <summary>
        /// This constructor fills the window with the information about the account.
        /// In case of nothing being given in not mandatory fields it displays "Not provided".
        /// </summary>
        public ManageAccountAdmin(UserLogin selectedAccount)
        {
            InitializeComponent();
            DataContext = this;
            SelectedAccount = selectedAccount;
            TextBoxUserName.Text = SelectedAccount.UserName;
            TextBoxEmail.Text = SelectedAccount.Email;
            TextBoxFirstName.Text = SelectedAccount?.FirstName ?? "Not provided";
            TextBoxLastName.Text = SelectedAccount?.LastName ?? "Not provided";
            TextBoxAge.Text = SelectedAccount?.Age ?? "Not provided";
            TextBoxPhoneNumber.Text = SelectedAccount?.PhoneNumber ?? "Not provided";
            TextBoxAddress.Text = SelectedAccount?.Address ?? "Not provided";
            TextBoxZipCode.Text = SelectedAccount?.ZipCode ?? "Not provided";
            TextBoxAccountType.Text = SelectedAccount!.AccountType;
        }
        /// <summary>
        /// This method allows "Administrator"s to change the UserName property of the selected user.
        /// If UserName is empty or already exists in database, it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetUserName(object sender, RoutedEventArgs e)
        {
            try
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
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.UserName = UserName;
                        SelectedAccount.UserName = UserName;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch(Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to change the Password property of a selected user.
        /// If input Password is empty it shows a message.
        /// If Password doesn't match the rules it shows a confirmation MessageBox (it will only be saved in case of user pressing "yes").
        /// </summary>
        private void ButtonClick_SetPassword(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password cannot be empty");
                }
                else if (!Password.Any(char.IsLower) || !Password.Any(char.IsUpper) || !Password.Any(char.IsNumber) || Password.Length < 8)
                {
                    MessageBox.Show("Password should follow the following rules:\nAt least one (lowercase and capital) letter is needed,\nAt least one number is needed,\nMust be at least 8 characters long.");
                    var messageBoxResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        using var db = new UsersContext();
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                        SelectedAccount.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                        db.SaveChanges();
                    }
                }
                else
                {
                    using var db = new UsersContext();
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                    SelectedAccount.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to change the Email property of a selected user.
        /// If input Email is empty, or of a wrong format it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    MessageBox.Show("Email cannot be empty");
                }
                else if (!IsValid(Email))
                {
                    MessageBox.Show("Wrong Email");
                }
                else
                {
                    using var db = new UsersContext();
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.Email = Email;
                    SelectedAccount.Email = Email;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the FirstName property of a selected user.
        /// If input FirstName is empty it shows a MessageBox.
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
                    using var db = new UsersContext();
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.FirstName = FirstName;
                    SelectedAccount.FirstName = FirstName;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current FirstName property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveFirstName(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.FirstName == null)
                    {
                        MessageBox.Show("First Name is already empty");
                    }
                    else
                    {
                        userData.FirstName = null;
                        SelectedAccount.FirstName = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the LastName property of a selected user.
        /// If input LastName is empty it shows a MessageBox.
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
                    using var db = new UsersContext();
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.LastName = LastName;
                    SelectedAccount.LastName = LastName;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current LastName property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveLastName(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.LastName == null)
                    {
                        MessageBox.Show("Last Name is already empty");
                    }
                    else
                    {
                        userData.LastName = null;
                        SelectedAccount.LastName = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the Age property of a selected user.
        /// If input Age is empty it shows a MessageBox.
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
                    using var db = new UsersContext();

                    var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.Age = Age;
                    SelectedAccount.Age = Age;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current Age property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveAge(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.Age == null)
                    {
                        MessageBox.Show("Age is already empty");
                    }
                    else
                    {
                        userData.Age = null;
                        SelectedAccount.Age = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the PhoneNumber property of a selected user.
        /// If input PhoneNumber is empty, of wrong length or contains a letter it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetPhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PhoneNumber.Length == 0)
                {
                    MessageBox.Show("You can't set empty Phone Number, if you want to remove Phone Number, press \"Remove\" button.");
                }
                else if (PhoneNumber.Length != 9 || PhoneNumber.Any(char.IsLetter))
                {
                    MessageBox.Show("Wrong Phone Number.");
                }
                else
                {
                    using var db = new UsersContext();

                            var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.PhoneNumber = PhoneNumber;
                    SelectedAccount.PhoneNumber = PhoneNumber;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current PhoneNumber property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemovePhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.PhoneNumber == null)
                    {
                        MessageBox.Show("Phone Number is already empty");
                    }
                    else
                    {
                        userData.PhoneNumber = null;
                        SelectedAccount.PhoneNumber = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the Address property of a selected user.
        /// If input Address is empty it shows a MessageBox.
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
                    using var db = new UsersContext();

                    var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.Address = Address;
                    SelectedAccount.Address = Address;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current Address property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveAddress(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.Address == null)
                    {
                        MessageBox.Show("Address is already empty");
                    }
                    else
                    {
                        userData.Address = null;
                        SelectedAccount.Address = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to set and change the ZipCode property of a selected user.
        /// If input ZipCode is empty or of a wrong format it shows a MessageBox.
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
                    using var db = new UsersContext();

                    var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    userData.ZipCode = ZipCode;
                    SelectedAccount.ZipCode = ZipCode;
                    db.SaveChanges();
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method allows "Administrator"s to remove current ZipCode property of a selected user from database.
        /// If the property was already empty it shows a MessageBox.
        /// </summary>
        private void ButtonClick_RemoveZipCode(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    if (userData?.ZipCode == null)
                    {
                        MessageBox.Show("Zip Code is already empty");
                    }
                    else
                    {
                        userData.ZipCode = null;
                        SelectedAccount.ZipCode = null;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method checks if "Administrator" is selected for AccountType property.
        /// </summary>
        private void ComboBoxAdministrator_Selected(object sender, RoutedEventArgs e)
        {
            AccountType = "Administrator";
            IsSelected = true;
        }
        /// <summary>
        /// This method checks if "Customer" is selected for AccountType property.
        /// </summary>
        private void ComboBoxCustomer_Selected(object sender, RoutedEventArgs e)
        {
            AccountType = "Customer";
            IsSelected = true;
        }
        /// <summary>
        /// This method allows "Administrator"s to change the AccountType property.
        /// If neither "Administrator" or "Customer" was selected, or AccountType wouldn't change it shows a MessageBox.
        /// If the process would override last existing "Administrator" account it shows a MessageBox.
        /// </summary>
        private void ButtonClick_SetAccountType(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!IsSelected)
                {
                    MessageBox.Show("Nothing was selected.");
                }
                else if (TextBoxAccountType.Text == AccountType)
                {
                    MessageBox.Show("No changes were made");
                }
                else
                {
                    using var db = new UsersContext();
                    var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                    var users = db.UserLogins
                        .Where(q => q.AccountType == "Administrator")
                        .ToList();
                    if (users.Count <= 1 && userData.AccountType == "Administrator" && AccountType == "Customer")
                    {
                        MessageBox.Show("You can't remove last Administrator Account");
                    }
                    else
                    {
                        userData.AccountType = AccountType;
                        SelectedAccount.AccountType = AccountType;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method checks if "Email" is of correct format using regex.
        /// </summary>
        /// <param name="email">Email given by user.</param>
        /// <returns>True if email is of correct format, otherwise false.</returns>
        private static bool IsValid(string email)
        {
            const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|pl)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// This method checks if "ZipCode" is of correct format using regex.
        /// </summary>
        /// <param name="zipCode">Zip code given by user.</param>
        /// <returns>True if zip code is of correct format, otherwise false.</returns>
        private static bool IsValidZipCode(string zipCode)
        {
            const string regex = @"^[0-9]{2}-[0-9]{3}$";
            return Regex.IsMatch(zipCode, regex);
        }
        /// <summary>
        /// This method updates data, by closing and reopening window.
        /// </summary>
        private void Update()
        {
            var _ = new ManageAccountAdmin(SelectedAccount);
            _.Show();
            DoClose = false;
            Close();
        }
        /// <summary>
        /// This method shows "AdminWelcomePageManageAccount" window if user actually intends to close the window.
        /// </summary>
        private void ManageAccountAdmin_Closing(object sender, CancelEventArgs e)
        {
            if (!DoClose) return;
            var _ = new AdminWelcomePageManageAccounts();
            _.Show();
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