using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
	public static class SeedData
	{
		public static void Seed()
		{
			using var db = new UsersContext();
			var userData = db.UserLogins
				.Where(q => q.AccountType == "Administrator")
				.ToList();
			if (userData.Count == 0)
			{
				db.UserLogins.Add(new UserLogin
				{
					UserName = "Admin",
					Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
					Email = "example@gmail.com",
					AccountType = "Administrator"
				});
				db.UserLogins.Add(new UserLogin
				{
					UserName = "johndoe123",
					Password = "Qwerty1234!",
					Email = "johndoe123@example.com",
					AccountType = "Customer"
				});
				db.UserLogins.Add(new UserLogin
				{
					UserName = "sarahjones22",
					Password = "Password1234@",
					Email = "sarahjones22@example.com",
					AccountType = "Customer"
				});
				db.Expenses.Add(new Expense
				{
					ExpensesName = "Groceries",
					Date = DateTime.Now,
					Amount = 10,
					TotalCost = 120.50f,
					CostPerSingle = (float)Math.Round(120.5f / 10, 2)
				});
				db.Expenses.Add(new Expense
				{
					ExpensesName = "Gasoline",
					Date = DateTime.Now,
					Amount = 5,
					TotalCost = 45.75f,
					CostPerSingle = (float)Math.Round(45.75f / 5, 2)
				});
				db.Stocks.Add(new Stock
				{
					Quantity = 2, DateCreated = DateTime.Now
				});
				db.Products.Add(new Product
				{
					ProductName = "Clothing", Price = (float)Math.Round(212.35f / 2, 2), StockId = 0
				});
			}
			db.SaveChanges();
		}
	}
}
