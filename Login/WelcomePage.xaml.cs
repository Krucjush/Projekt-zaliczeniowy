using System.Windows;

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Window
    {

        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Button_Click_Manage_Account(object sender, RoutedEventArgs e)
        {
            var manageAccount = new ManageAccount();
            manageAccount.Show();
        }
    }
}
