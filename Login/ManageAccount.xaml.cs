using System;
using System.Collections.Generic;
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

namespace Program
{
    /// <summary>
    /// Logika interakcji dla klasy ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
        public string ZipCode { get; set; }

        public ManageAccount()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .Where(q => q.UserName == LoginWindow.UserName)
                    .ToList()
                    .Last();
                TextBlockUserName.Text = userData.UserName;
                TextBlockPassword.Text = userData.Password;
                TextBlockEmail.Text = userData.Email;
                TextBlockFirstName.Text = userData?.FirstName ?? "Not provided";
                TextBlockLastName.Text = userData?.LastName ?? "Not provided";
                TextBlockAge.Text = userData.Age != 0 ? userData?.Age.ToString() : "Not provided";
                TextBlockPhoneNumber.Text = userData.PhoneNumber != 0 ? userData?.Age.ToString() : "Not provided";
                TextBlockZipCode.Text = userData?.ZipCode ?? "Not provided";
            }
        }
    }
}
