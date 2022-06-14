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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public class UsersContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public UsersContext() : base("InternetStore")
        {

        }
    }
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
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public string OrderStatus { get; set; }

        public long Id { get; set; }
        [ForeignKey("Id")]
        public UserLogin UserLogin { get; set; }

        public long StockId { get; set; }
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }

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

    public class OrderItem
    {
        [Key, Column(Order = 0)]
        public long OrderId { get; set; }
        [Key, Column(Order = 1)]
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public float Price { get; set; }

        [ForeignKey("OrderId")]
        public Order Orders { get; set; }

        public long? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }

    }
    public class ProductInStore
    {
        public long Available { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public long? AmountInCart { get; set; }
    }
    public class StockTable
    {
        public long StockId { get; set; }
        public string ItemName { get; set; }
        public long Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
    public class ProductTable
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
    }
    public class CartItem
    {
        public long Amount { get; set; }
        public string ProductName { get; set; }
        public float Cost { get; set; }
        public float TotalCost { get; set; }
    }
}
