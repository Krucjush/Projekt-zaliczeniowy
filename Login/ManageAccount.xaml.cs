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
using Login;

namespace Program
{
    /// <summary>
    /// Logika interakcji dla klasy ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : Window
    {
        public ManageAccount()
        {
            InitializeComponent();
            using (var db = new UsersContext())
            {
                var userData = db.UserLogins
                    .SingleOrDefault(q => q.UserName == LoginWindow.UserName);
                TextBlockUserName.Text = userData.UserName;
                TextBlockPassword.Text = userData.Password;
                TextBlockEmail.Text = userData.Email;
                TextBlockFirstName.Text = userData?.FirstName ?? "Not provided";
                TextBlockLastName.Text = userData?.LastName ?? "Not provided";
                TextBlockAge.Text = userData?.Age != 0 ? userData?.Age.ToString() : "Not provided";
                TextBlockPhoneNumber.Text = userData?.PhoneNumber != 0 ? userData?.PhoneNumber.ToString() : "Not provided";
                TextBlockAddress.Text = userData?.Address ?? "Not provided";
                TextBlockZipCode.Text = userData?.ZipCode ?? "Not provided";
            }
        }
        private void Button_Click_Manage_User_Name(object sender, RoutedEventArgs e)
        {
            var manage = new ManageUserName();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Password(object sender, RoutedEventArgs e)
        {
            var manage = new ManagePassword();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Email(object sender, RoutedEventArgs e)
        {
            var manage = new ManageEmail();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_First_Name(object sender, RoutedEventArgs e)
        {
            var manage = new ManageFirstName();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Last_Name(object sender, RoutedEventArgs e)
        {
            var manage = new ManageLastName();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Age(object sender, RoutedEventArgs e)
        {
            var manage = new ManageAge();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Phone_Number(object sender, RoutedEventArgs e)
        {
            var manage = new ManagePhoneNumber();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Address(object sender, RoutedEventArgs e)
        {
            var manage = new ManageUserName();
            manage.Show();
            Close();
        }
        private void Button_Click_Manage_Zip_Code(object sender, RoutedEventArgs e)
        {
            var manage = new ManageUserName();
            manage.Show();
            Close();
        }
    }
}
