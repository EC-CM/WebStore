namespace WebStore.Migrations
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebStore.Models;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Contexts;
    using WebStore.Services;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebStore.Models.DatabaseContext context)
        // Called after migrating to the latest database version
        {
            ResetAllIDs(context);
            Console.WriteLine("[#] Reset table ID columns.");

            using (ImageService imageService = new ImageService())
            {
                imageService.SeedImages();
            }

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category { Name = "TVs", Description = "Elevate your entertainment experience with our cutting-edge TVs. Immerse yourself in stunning visuals, vibrant colors, and crystal-clear sound. Whether you're into thrilling movies, immersive gaming, or catching up on your favorite shows, our range of TVs delivers an unparalleled viewing experience.", ImageID = 2 },
                new Category { Name = "Monitors", Description = "Boost your productivity and enhance your digital workspace with our sleek monitors. Designed for clarity and performance, our monitors deliver crisp visuals and responsive displays. Whether you're working, gaming, or creating, our monitors provide the visual precision you need for every task.", ImageID = 3 },
                new Category { Name = "Smartphones", Description = "Stay connected and empowered with our range of smartphones. Packed with innovative features and the latest technology, our smartphones offer seamless communication, stunning photography, and versatile functionality. Experience the convenience of a smart and connected lifestyle in the palm of your hand.", ImageID = 4 },
                new Category { Name = "Headphones", Description = "Immerse yourself in a world of exceptional audio with our premium headphones. Whether you crave high-fidelity sound for music, need a reliable headset for calls, or seek immersive gaming audio, our selection of headphones delivers superior comfort and exceptional sound quality. Elevate your auditory experience with our top-notch headphone collection.", ImageID = 5 }
            );
            Console.WriteLine("[#] Categories updated.");

            context.Users.AddOrUpdate(
                u => u.Username,
                new User { Username = "Admin", EmailAddress = "admin@techstore.com", PasswordHash = "", Forename = "Administrator", Surname = "", ProfilePictureID = 6,},
                new User { Username = "Guest", EmailAddress = "guest@techstore.com", PasswordHash = "", Forename = "Guest", Surname = "", ProfilePictureID = 7, }
                );
            Console.WriteLine("[#] Users updated.");

        }


        public void ResetAllIDs(DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Categories', RESEED, {(context.Categories.Max(e => (int?)e.CategoryID) ?? 0)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Images', RESEED, {(context.Images.Max(e => (int?)e.ImageID) ?? 0)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('ProductImages', RESEED, {(context.ProductImages.Max(e => (int?)e.ProductImageID) ?? 0)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Products', RESEED, {(context.Products.Max(e => (int?)e.ProductID) ?? 0)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Users', RESEED, {(context.Users.Max(e => (int?)e.UserID) ?? 0)})");
        }


        /*
        public void ResetIdentitySeed<T>(DbContext context, string tableName, DbSet<T> dbSet) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var tableName = entityType.GetTableName();
            var identityColumnName = entityType.FindPrimaryKey().Properties.First().Name;

            var maxId = context.Set<T>().Max(e => EF.Property<int>(e, identityColumnName));

            var sqlCommand = $"DBCC CHECKIDENT('{tableName}', RESEED, {maxId});";
            context.Database.ExecuteSqlCommand(sqlCommand);
        }
        */

    }
}