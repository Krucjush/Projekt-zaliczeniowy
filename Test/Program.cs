using System;
using System.Linq;

namespace Test
{
    public class Program
    {
        public static void Main()
        {
            // Hint: change `DESKTOP-123ABC\SQLEXPRESS` to your server name
            //       alternatively use `localhost` or `localhost\SQLEXPRESS`

            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=blogdb;Integrated Security=True";

            using (BloggingContext db = new BloggingContext(connectionString))
            {
                Console.WriteLine($"Database ConnectionString: {db.ConnectionString}.");

                // Create
                Console.WriteLine("Inserting a new blog");

                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                //// Read
                //Console.WriteLine("Querying for a blog");

                //Blog blog = db.Blogs
                //    .OrderBy(b => b.Id)
                //    .First();

                //// Update
                //Console.WriteLine("Updating the blog and adding a post");

                //blog.Url = "https://devblogs.microsoft.com/dotnet";
                //blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                //db.SaveChanges();

                //// Delete
                //Console.WriteLine("Delete the blog");

                //db.Remove(blog);
                //db.SaveChanges();
            }
        }
    }
}