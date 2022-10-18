using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Login
{
    /// <summary>
    /// This class contains information of all available tables in database, and name or connection string if needed.
    /// </summary>
    
    public class UsersContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //change to connection string when needed
        //Data Source=PK1-20;Initial Catalog=InternetStore;Integrated Security=True Data Source=PK1-20;Integrated Security=True^
        public UsersContext() : base("InternetStore")
        {

        }
    }
    /// <summary>
    /// This class contains information about the UserLogin table.
    /// </summary>
    public class UserLogin
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public List<Order> Orders { get; set; }
    }
    /// <summary>
    /// This class contains information about Order table.
    /// </summary>
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public string OrderStatus { get; set; }
        //"Pending", "Processing", "Rejected", "Completed"
        public bool Payment { get; set; }
        public string ShippingMethod { get; set; }
        //"Package", "Courier", "Parcel locker", "Pickup at the point"

        public long Id { get; set; }
        [ForeignKey("Id")]
        public UserLogin UserLogin { get; set; }

        public long StockId { get; set; }
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
    /// <summary>
    /// This class contains information about the Expense table.
    /// </summary>
    public class Expense
    {
        [Key]
        public long ExpenseId { get; set; }
        [Required]
        public string ExpensesName { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public long Amount { get; set; }
        public long TotalCost { get; set; }
        public float CostPerSingle { get; set; }
    }
    /// <summary>
    /// This class contains information about the Stock table.
    /// </summary>
    public class Stock
    {
        [Key]
        public long StockId { get; set; }
        [Required]
        public long Quantity { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
    /// <summary>
    /// This class contains information about the Product table.
    /// </summary>
    public class Product
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }

        public long StockId { get; set; }
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
    /// <summary>
    /// This class contains information about the OrderItem table.
    /// </summary>
    public class OrderItem
    {
        [Key]
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public float Price { get; set; }

        public long OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Orders { get; set; }

        public long? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }

    }
    /// <summary>
    /// This class contains information about the products in store.
    /// Created, because none of the tables above were designed for it.
    /// Used only in WelcomePage class.
    /// </summary>
    public class ProductInStore
    {
        public long Available { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public long? AmountInCart { get; set; }
    }
    /// <summary>
    /// This class contains information about stocks.
    /// Created, because none of the tables above were designed for it.
    /// Used only in AdminWelcomePageManageStocks class.
    /// </summary>
    public class StockTable
    {
        public long StockId { get; set; }
        public string ItemName { get; set; }
        public long Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
    /// <summary>
    /// This class contains information about Products.
    /// Created, because none of the tables above were designed for it.
    /// Used only in AdminWelcomePageManageProducts class.
    /// </summary>
    public class ProductTable
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
    }
    /// <summary>
    /// This class contains information about products in cart.
    /// Created, because none of the tables above were designed for it.
    /// Used in ShoppingCart and WelcomePage classes.
    /// </summary>
    public class CartItem
    {
        public long Amount { get; set; }
        public string ProductName { get; set; }
        public float Cost { get; set; }
        public float TotalCost { get; set; }
    }
    /// <summary>
    /// This class contains information about orders.
    /// Created, because none of the tables above were designed for it.
    /// Used only in AdminWelcomePageManageOrders class.
    /// </summary>
    public class OrdersTable
    {
        public long OrderId { get; set; }
        public string OrderStatus { get; set; }
        public long UserId { get; set; }
    }
    /// <summary>
    /// This class contains information about order items.
    /// Created, because none of the tables above were designed for it.
    /// Used only in OrderInformation class.
    /// </summary>
    public class OrderItemsTable
    {
        public long Quantity { get; set; }
        public string ItemName { get; set; }
        public float Cost { get; set; }
    }
}