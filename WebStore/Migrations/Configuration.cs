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
    using System.Web.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Models.DatabaseContext>
    {
        private DatabaseContext _context;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(WebStore.Models.DatabaseContext context)
        // Called after migrating to the latest database version
        {
            // If update-database errors: Either set seed to 0 or 1
            ResetAllIDs(context, seed:0);
            Console.WriteLine("[#] Reset table ID columns.");

            using (ImageService imageService = new ImageService())
            {
                imageService.SeedImages();
            }

            context.SaveChanges();

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
                new User { Username = "Admin", EmailAddress = "admin@techstore.com", PasswordHash = "", Forename = "Administrator", Surname = "", ProfilePictureID = 6, Role = "Admin" },
                new User { Username = "Guest", EmailAddress = "guest@techstore.com", PasswordHash = "", Forename = "Guest", Surname = "", ProfilePictureID = 7, Role = "Guest" }
                );
            Console.WriteLine("[#] Users updated.");


            context.Products.AddOrUpdate(
                p => p.Name,
                new Product { Name = "SONY BRAVIA XR-55A95LU 55\" Smart 4K Ultra HD HDR OLED TV", Description = "", Price = 2499.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Bravia_XR-55A95LU-1.jpg"), CategoryID = 1 },
                new Product { Name = "SONY BRAVIA XR-55A80LU 55\" Smart 4K Ultra HD HDR OLED TV", Description = "", Price = 1599.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Bravia_XR55A80LU-1.jpg"), CategoryID = 1 },
                new Product { Name = "Samsung S90C 55\" 4K OLED HDR Smart TV (2023)", Description = "", Price = 1129.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_S90C-1.png"), CategoryID = 1 },
                new Product { Name = "LG OLED evo C3 55\" 4K OLED HDR Smart TV (OLED55C34LA)", Description = "", Price = 1069.00M, DefaultImageID = GetImageIDFromName(context, "LG_OLED55C3-1.jpg"), CategoryID = 1 },
                new Product { Name = "LG OLED evo G3 55\" 4K OLED HDR Smart TV (OLED55G36LA)", Description = "", Price = 1399.00M, DefaultImageID = GetImageIDFromName(context, "LG-OLED55G36LA-1.jpg"), CategoryID = 1 },
                new Product { Name = "HISENSE 55\" Smart 4K Ultra HD HDR Mini-LED TV (55U8KQTUK)", Description = "", Price = 999.00M, DefaultImageID = GetImageIDFromName(context, "Hisense_55U8K-1.png"), CategoryID = 1 },
                new Product { Name = "Alienware 34\" WQHD 21:9 1800R QD OLED 165Hz Curved Gaming Monitor (AW3423DWF)", Description = "", Price = 740.33M, DefaultImageID = GetImageIDFromName(context, "Alienware_AW3423DWF-1.jpg"), CategoryID = 2 },
                new Product { Name = "AOC Gaming 34\" WQHD 144Hz Curved Monitor (CU34G2X)", Description = "", Price = 317.97M, DefaultImageID = GetImageIDFromName(context, "AOC_CU34G2-1.png"), CategoryID = 2 },
                new Product { Name = "Corsair Xeneon Flex 45\" OLED WQHD Bendable Display Gaming Monitor (45WQHD240 )", Description = "", Price = 1499.98M, DefaultImageID = GetImageIDFromName(context, "Corsair_Xeneon_Flex_45WQHD240-1.jpg"), CategoryID = 2 },
                new Product { Name = "LG UltraGear 21:9 UW-QHD 160Hz Nano IPS 37.5\" Curved Gaming Monitor (38GN950)", Description = "", Price = 899.98M, DefaultImageID = GetImageIDFromName(context, "LG_UltraGear_38GN950-1.jpg"), CategoryID = 2 },
                new Product { Name = "Samsung Odyssey Neo G9 49\" DQHD 32:9 Quantum Mini-LED Gaming Monitor", Description = "", Price = 1350.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Odyssey_Neo_G9-1.jpg"), CategoryID = 2 },
                new Product { Name = "Samsung Odyssey OLED G9 49\" 240Hz 32:9 Smart Gaming Monitor", Description = "", Price = 1399.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Odyssey_OLED_G9-1.png"), CategoryID = 2 },
                new Product { Name = "Sony Xperia 1 V", Description = "", Price = 1249.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Xperia_1_V-01.jpg"), CategoryID = 3 },
                new Product { Name = "Sony Xperia 5 V", Description = "", Price = 849.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Xperia_5_V-1.jpg"), CategoryID = 3 },
                new Product { Name = "ROG Phone 7 Ultimate", Description = "", Price = 1199.99M, DefaultImageID = GetImageIDFromName(context, "Asus_ROG_Phone_7_Ultimate-1.jpg"), CategoryID = 3 },
                new Product { Name = "Samsung S4 Ultra 512GB", Description = "", Price = 1349.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Galaxy_S24_Ultra-1.jpg"), CategoryID = 3 },
                new Product { Name = "Google Pixel 8 Pro", Description = "", Price = 999.00M, DefaultImageID = GetImageIDFromName(context, "Google_Pixel_8_Pro-1.png"), CategoryID = 3 },
                new Product { Name = "Sony WH-1000XM5 Wireless Noise Cancelling Headphones", Description = "", Price = 299.00M, DefaultImageID = GetImageIDFromName(context, "Sony_WH-1000XM5-1.jpg"), CategoryID = 4 },
                new Product { Name = "Sony WH-1000XM4 Wireless Noise Cancelling Headphones", Description = "", Price = 249.00M, DefaultImageID = GetImageIDFromName(context, "Sony_WH-1000XM4-1.jpg"), CategoryID = 4 },
                new Product { Name = "Sennheiser MOMENTUM 4 Wireless Headphones", Description = "", Price = 249.99M, DefaultImageID = GetImageIDFromName(context, "Sennheiser_MOMENTUM_4-1.jpg"), CategoryID = 4 },
                new Product { Name = "Bose QuietComfort Ultra Wireless Noise Cancelling Headphones", Description = "", Price = 449.00M, DefaultImageID = GetImageIDFromName(context, "Bose_Quietcomfort_Ultra-Headphones-1.png"), CategoryID = 4 }
            );
            Console.WriteLine("[#] Products updated.");


            // Loop through all Products
            foreach (Product product in context.Products.ToList())
            {
                // Using the associated DefaultImageID to locate the image file name in Images
                string imageFormat = context.Images
                    .Where(image => image.ImageID == product.DefaultImageID)
                    .Select(image => image.ImageName)
                    .FirstOrDefault();
                    //.Split('-)[0]

                // And remove the image number
                int indexOfImageNumber = imageFormat.LastIndexOf('-');
                if (indexOfImageNumber != -1)
                {
                    imageFormat = imageFormat.Substring(0, indexOfImageNumber);
                }

                if (imageFormat != null)
                {
                    // Store all images matching that image format
                    List<Image> pImages = context.Images
                        .Where(image => image.ImageName.StartsWith(imageFormat))
                        .OrderBy(image => image.ImageID)
                        .ToList();


                    foreach (Image pImage in pImages)
                    {
                        // Create a new ProductImage for that link
                        context.ProductImages.AddOrUpdate(
                            pi => new { pi.ProductID, pi.ImageID },
                            new ProductImage
                            {
                                ProductID = product.ProductID,
                                ImageID = pImage.ImageID
                            }
                        );

                    }   

                    Console.WriteLine($" - Added {pImages.Count()} images to {product.Name} matching \"{imageFormat}-#\" ");
                }
            }
            Console.WriteLine("[#] ProductImages updated.");

            context.SaveChanges();





        }


        public void ResetAllIDs(DatabaseContext context, int seed=0)
        {
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Categories', RESEED, {(context.Categories.Max(e => (int?)e.CategoryID) ?? seed)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Images', RESEED, {(context.Images.Max(e => (int?)e.ImageID) ?? seed)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('ProductImages', RESEED, {(context.ProductImages.Max(e => (int?)e.ProductImageID) ?? seed)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Products', RESEED, {(context.Products.Max(e => (int?)e.ProductID) ?? seed)})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Users', RESEED, {(context.Users.Max(e => (int?)e.UserID) ?? seed)})");

            context.SaveChanges();
        }

        public int GetImageIDFromName(DatabaseContext context, string name)
        {
            int imageID = context.Images
                .Where(image => image.ImageName == name)
                .Select(image => image.ImageID)
                .FirstOrDefault();

            return imageID;
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