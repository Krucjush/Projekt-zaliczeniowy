using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Program;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy ManageAccountAdmin.xaml
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
        public ManageAccountAdmin(UserLogin selectedAccount)
        {
            InitializeComponent();
            DataContext = this;
            SelectedAccount = selectedAccount;
            TextBoxUserName.Text = SelectedAccount.UserName;
            TextBoxPassword.Text = SelectedAccount.Password;
            TextBoxEmail.Text = SelectedAccount.Email;
            TextBoxFirstName.Text = SelectedAccount?.FirstName ?? "Not provided";
            TextBoxLastName.Text = SelectedAccount?.LastName ?? "Not provided";
            TextBoxAge.Text = SelectedAccount?.Age ?? "Not provided";
            TextBoxPhoneNumber.Text = SelectedAccount?.PhoneNumber ?? "Not provided";
            TextBoxAddress.Text = SelectedAccount?.Address ?? "Not provided";
            TextBoxZipCode.Text = SelectedAccount?.ZipCode ?? "Not provided";
            TextBoxAccountType.Text = SelectedAccount.AccountType;
        }
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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetPassword(object sender, RoutedEventArgs e)
        {
            try
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
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.Password = Password;
                        SelectedAccount.Password = Password;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetEmail(object sender, RoutedEventArgs e)
        {
            try
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
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.Email = Email;
                        SelectedAccount.Email = Email;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetAge(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (string.IsNullOrEmpty(Age))
                    {
                        MessageBox.Show("You can't set empty Age, if you want to remove Age, press \"Remove\" button");
                    }
                    else
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.Age = Age;
                        SelectedAccount.Age = Age;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetPhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (PhoneNumber.Length == 0)
                    {
                        MessageBox.Show("You can't set empty Phone Number, if you want to remove Phone Number, press \"Remove\" button.");
                    }
                    else if (PhoneNumber.Length != 9)
                    {
                        MessageBox.Show("Wrong Phone Number.");
                    }
                    else
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.PhoneNumber = PhoneNumber;
                        SelectedAccount.PhoneNumber = PhoneNumber;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetAddress(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (string.IsNullOrEmpty(Address))
                    {
                        MessageBox.Show("You can't set empty Address, if you want to remove Address, press \"Remove\" button.");
                    }
                    else
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.Address = Address;
                        SelectedAccount.Address = Address;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ButtonClick_SetZipCode(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    if (string.IsNullOrEmpty(ZipCode))
                    {
                        MessageBox.Show("You can't set empty Zip Code, if you want to remove Zip Code, press \"Remove\" button.");
                    }
                    else if (!ZipCode.Contains("-") && ZipCode.Length != 6)
                    {
                        MessageBox.Show("Wrong Zip Code");
                    }
                    else
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
                        userData.ZipCode = ZipCode;
                        SelectedAccount.ZipCode = ZipCode;
                        db.SaveChanges();
                    }
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }

        private void ComboBoxAdministrator_Selected(object sender, RoutedEventArgs e)
        {
            AccountType = "Administrator";
            IsSelected = true;
        }

        private void ComboBoxCustomer_Selected(object sender, RoutedEventArgs e)
        {
            AccountType = "Customer";
            IsSelected = true;
        }

        private void ButtonClick_SetAccountType(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var userData = db.UserLogins
                        .SingleOrDefault(q => q.UserName == SelectedAccount.UserName);
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
                }
                Update();
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                Close();
            }
        }
        private void Update()
        {
            var _ = new ManageAccountAdmin(SelectedAccount);
            _.Show();
            DoClose = false;
            Close();
        }

        private void ManageAccountAdmin_Closing(object sender, CancelEventArgs e)
        {
            if (!DoClose) return;
            var _ = new AdminWelcomePageManageAccounts();
            _.Show();
        }
    }
}
