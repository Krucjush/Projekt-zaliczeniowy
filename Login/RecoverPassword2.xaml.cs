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
    /// Logika interakcji dla klasy RecoverPassword2.xaml
    /// </summary>
    public partial class RecoverPassword2 : Window
    {
        public string UserName { get; set; }
        private readonly Random _random = new();
        public RecoverPassword2(string userName)
        {
            UserName = userName;
            InitializeComponent();
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            if ((bool)EmailRb.IsChecked)
            {
                var rightCode = "";
                for (var i = 0; i < 7; i++)
                {
                    var n = _random.Next(0, 10);
                    rightCode += n.ToString();
                }
                var _ = new RecoverPasswordEmail(rightCode, UserName);
                _.Show();
                Close();
            }
            else if ((bool)PhoneRb.IsChecked)
            {
                //var _ = new RecoverPasswordPhone();
                //_.Show();
                //Close();
            }
            else if ((bool)NoneRb.IsChecked)
            {
                MessageBox.Show("We cannot confirm it's really you.");
                var _ = new LoginWindow();
                _.Show();
                Close();
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            var _ = new RecoverPassword();
            _.Show();
            Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            var _ = new LoginWindow();
            _.Show();
            Close();
        }
    }
}
