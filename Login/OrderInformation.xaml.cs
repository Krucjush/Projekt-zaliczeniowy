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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Login
{
    /// <summary>
    /// This class is available only for "Customer"s.
    /// It allows them to place orders, choose delivery type and payment method.
    /// </summary>
    public partial class OrderInformation : Window
    {
        public bool DoClose { get; set; } = true;
        public string ShippingMethod { get; set; }
        public bool IsSelected { get; set; }
        /// <summary>
        /// This constructor generates binding and fills information in the window.
        /// </summary>
        public OrderInformation()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                using var db = new UsersContext();
                var user = db.UserLogins
                    .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
                var _ = db.OrderItems
                    .Where(q => q.Orders.OrderStatus == "Pending")
                    .Select(q => new OrderItemsTable { Quantity = q.Quantity, ItemName = q.Products.ProductName, Cost = (float)Math.Round(q.Price * q.Quantity, 2)})
                    .ToList();
                TextBoxFullName.Text = user.LastName + " " + user.LastName;
                TextBoxAddress.Text = user.Address;
                TextBoxZipCode.Text = user.ZipCode;
                OrderItemsData.ItemsSource = _;
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method checks if "PackageShipping" shipping method is selected.
        /// </summary>
        private void PackageShipping_Selected(object sender, RoutedEventArgs e)
        {
            ShippingMethod = "PackageShipping";
            IsSelected = true;
        }
        /// <summary>
        /// This method checks if "CourierShipping" shipping method is selected.
        /// </summary>
        private void CourierShipping_Selected(object sender, RoutedEventArgs e)
        {
            ShippingMethod = "Courier Shipping";
            IsSelected = true;
        }
        /// <summary>
        /// This method checks if "ParcelLockerShipping" shipping method is selected.
        /// </summary>
        private void ParcelLockerShipping_Selected(object sender, RoutedEventArgs e)
        {
            ShippingMethod = "Parcel Locker Shipping";
            IsSelected = true;
        }
        /// <summary>
        /// This method checks if "PickupAtThePoint" shipping method is selected
        /// </summary>
        private void PickupAtThePoint_Selected(object sender, RoutedEventArgs e)
        {
            ShippingMethod = "Pickup at the point";
            IsSelected = true;
        }
        /// <summary>
        /// This method check which payment method is selected.
        /// If shipment method hasn't ben chosen it shows a MessageBox.
        /// </summary>
        private void Buttons(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsSelected)
                {
                    MessageBox.Show("Please select Shipment method.");
                }
                else
                {
                    using (var db = new UsersContext())
                    {
                        var user = db.UserLogins
                            .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
                        var order = db.Orders
                            .Where(q => q.Id == user.Id)
                            .FirstOrDefault(q => q.OrderStatus == "Pending");
                        order.ShippingMethod = ShippingMethod;
                        order.OrderStatus = "Processing";
                        order.Payment = true;
                        db.SaveChanges();
                    }
                    MessageBox.Show("Order Placed.");
                    var _ = new WelcomePage();
                    _.Show();
                    DoClose = false;
                    Close();
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }
        /// <summary>
        /// This method changes the OrderStatus to "Rejected" if user intentionally closes the application.
        /// It is used to prevent exploiting getting items "for free".
        /// </summary>
        private void OrderInformation_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (!DoClose) return;
                using var db = new UsersContext();
                var user = db.UserLogins
                    .FirstOrDefault(q => q.UserName == LoginWindow.UserName);
                var order = db.Orders
                    .Where(q => q.Id == user.Id)
                    .FirstOrDefault(q => q.OrderStatus == "Pending");
                order.OrderStatus = "Rejected";
                order.Payment = false;
                db.SaveChanges();
            }
            catch (Exception exception)
            {
                Error(exception);
            }
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