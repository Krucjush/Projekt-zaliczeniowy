using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Program
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public class UsersContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Expense> Expenses { get; set; }

    }
    public class UserLogin
    {
        [Key]
        public long Id { get; set; }
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }
        [Required()]
        public string Email { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
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
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}
