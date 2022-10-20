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

namespace Login
{
    /// <summary>
    /// This class allows all users to recover access the their accounts.
    /// </summary>
    public partial class RecoverPassword : Window
    {
        public string UserName { get; set; }
        /// <summary>
        /// This constructor generates binding.
        /// </summary>
        public RecoverPassword()
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
        /// This method opens next form for password recovery.
        /// If UserName has not been given or doesn't exist in database it shows a MessageBox.
        /// </summary>
        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new UsersContext();
                var userNames = db.UserLogins
                    .Select(q => q.UserName)
                    .ToList();
                if (string.IsNullOrEmpty(UserName))
                {
                    MessageBox.Show("Provide User Name.");
                }
                else if (!userNames.Contains(UserName))
                {
                    MessageBox.Show("Wrong User Name.");
                }
                else
                {
                    var _ = new RecoverPassword2(UserName);
                    _.Show();
                    Close();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method opens LoginWindow.
        /// </summary>
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ = new LoginWindow();
                _.Show();
                Close();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method shows information or error
        /// </summary>
        private void Error(Exception exception)
        {
            MessageBox.Show("something went wrong\n" + exception);
            Close();
        }
    }
}
