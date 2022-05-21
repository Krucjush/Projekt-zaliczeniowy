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
        public DbSet<CustomerData> customerDatas { get; set; }
        //public string ConnectionString { get; }
        //public UsersContext(string connectionString)
        //{
        //    ConnectionString = connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(ConnectionString);
        //}
    }
    public class UserLogin
    {
        public long Id { get; set; }
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }
        [Required()]
        public string Email { get; set; }
        public string AccountType { get; set; }
    }
    public class CustomerData
    {
        [ForeignKey("UserLoginForeignKey")]
        public long Id { get; set; }
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string ZipCode { get; set; }
    }
}
