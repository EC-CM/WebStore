using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class DatabaseContext : DbContext
    {
        // Linking all models to tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        // Constructor to specify connection string
        public DatabaseContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;" +
                                         "Initial Catalog=WebStore;" + // DATABASE NAME
                                         "Integrated Security=True;" +
                                         "Connect Timeout=30;" +
                                         "Encrypt=False;" +
                                         "TrustServerCertificate=False;" +
                                         "ApplicationIntent=ReadWrite;" +
                                         "MultiSubnetFailover=False")
        {
            // Automatic database creation if the database doesn't exist
            Database.SetInitializer(new CreateDatabaseIfNotExists<DatabaseContext>());

        }

    }
}