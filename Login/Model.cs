using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        public string ConnectionString { get; set; }

        public UsersContext() : base("InternetStore")
        {
            ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog = InternetStore;Integrated Security = True";
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
    }
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public long Id { get; set; }
        [ForeignKey("Id")]
        public UserLogin UserLogin { get; set; }
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
    }

    public class Stock
    {
        [Key]
        public long StockId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public long Quantity { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public long ExpenseId { get; set; }
        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; }
    }
}
