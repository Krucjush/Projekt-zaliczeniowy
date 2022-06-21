using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Klasa do zarzadania kontem
    /// Widok dla zwyklych uzytkownikow (nie administratorow)
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
                TextBoxPassword.Text = userData.Password;
                TextBoxEmail.Text = userData.Email;
                TextBoxFirstName.Text = userData?.FirstName ?? "Not provided";
                TextBoxLastName.Text = userData?.LastName ?? "Not provided";
                TextBoxAge.Text = userData?.Age ?? "Not provided";
                TextBoxPhoneNumber.Text = userData?.PhoneNumber ?? "Not provided";
                TextBoxAddress.Text = userData?.Address ?? "Not provided";
                TextBoxZipCode.Text = userData?.ZipCode ?? "Not provided";
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

        private void ButtonClick_SetPassword(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password cannot be empty");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var userData = db.UserLogins
                            .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                        userData.Password = Password;
                        db.SaveChanges();
                    }
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

        private void ButtonClick_SetEmail(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

        private void ButtonClick_SetPhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PhoneNumber.Length == 0)
                {
                    MessageBox.Show("You can't set empty Phone Number, if you want to remove Phone Number, press \"Remove\" button.");
                }
                else if(PhoneNumber.Length != 9)
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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

        private void ButtonClick_SetZipCode(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }

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
            catch
            {
                MessageBox.Show("Something went wrong");
                DoClose = false;
                Close();
            }
        }
        private void Update()
        {
            var _ = new ManageAccount();
            _.Show();
            DoClose = false;
            Close();
        }
        private void ManageAccount_Closing(object sender, CancelEventArgs e)
        {
            if (!DoClose) return;
            var _ = new WelcomePage();
            _.Show();
        }
    }
}
