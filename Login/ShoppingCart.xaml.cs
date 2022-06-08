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

namespace Login
{
    /// <summary>
    /// Logika interakcji dla klasy ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window
    {
        public bool DoClose { get; set; } = true;
        public List<Product> Cart { get; set; }
        public ShoppingCart(List<Product> cart)
        {
            InitializeComponent();
            Cart = cart;
            Products.ItemsSource = Cart;
        }

        private void ButtonClick_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonClick_Remove(object sender, RoutedEventArgs e)
        {
            var row = (Product)Products.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Item not selected");
            }
            else
            {
                Cart.Remove(row);
                DoClose = false;
                Update();
            }
        }
        private void Update()
        {
            Close();
            var _ = new ShoppingCart(Cart);
            _.Show();
        }

        private void ShoppingCart_Closing(object sender, CancelEventArgs e)
        {
            if(!DoClose) return;
            var _ = new WelcomePage();
            _.Show();
        }
    }
}
