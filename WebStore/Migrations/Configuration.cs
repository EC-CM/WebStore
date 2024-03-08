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
    using System.Data.SqlTypes;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Models.DatabaseContext>
    {
        //private DatabaseContext _context;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(WebStore.Models.DatabaseContext context)
        // Called after migrating to the latest database version
        {
            // If update-database errors: Either set seed to 0 or 1
            ResetAllIDs(context);
            Console.WriteLine("[#] Reset table ID columns.");

            Console.WriteLine("[#] Updating Images...");
            using (ImageService imageService = new ImageService())
            {
                imageService.SeedImages();
            }

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category { Name = "TVs", Description = "Elevate your entertainment experience with our cutting-edge TVs. Immerse yourself in stunning visuals, vibrant colors, and crystal-clear sound. Whether you're into thrilling movies, immersive gaming, or catching up on your favorite shows, our range of TVs delivers an unparalleled viewing experience.", ImageID = GetImageIDFromName(context, "Category_TVs.jpg") },
                new Category { Name = "Monitors", Description = "Boost your productivity and enhance your digital workspace with our sleek monitors. Designed for clarity and performance, our monitors deliver crisp visuals and responsive displays. Whether you're working, gaming, or creating, our monitors provide the visual precision you need for every task.", ImageID = GetImageIDFromName(context, "Category_Monitors.jpg") },
                new Category { Name = "Smartphones", Description = "Stay connected and empowered with our range of smartphones. Packed with innovative features and the latest technology, our smartphones offer seamless communication, stunning photography, and versatile functionality. Experience the convenience of a smart and connected lifestyle in the palm of your hand.", ImageID = GetImageIDFromName(context, "Category_Smartphones.png") },
                new Category { Name = "Headphones", Description = "Immerse yourself in a world of exceptional audio with our premium headphones. Whether you crave high-fidelity sound for music, need a reliable headset for calls, or seek immersive gaming audio, our selection of headphones delivers superior comfort and exceptional sound quality. Elevate your auditory experience with our top-notch headphone collection.", ImageID = GetImageIDFromName(context, "Category_Headphones.jpg") }
            );
            context.SaveChanges();
            Console.WriteLine("[#] Categories updated.");

            context.Users.AddOrUpdate(
                u => u.Username,
                new User { Username = "Admin", EmailAddress = "admin@techstore.com", PasswordHash = "", Forename = "Administrator", Surname = "", ProfilePictureID = GetImageIDFromName(context, "User_Admin.jpg"), Role = User.UserRole.Admin },
                new User { Username = "Guest", EmailAddress = "guest@techstore.com", PasswordHash = "", Forename = "Guest", Surname = "", ProfilePictureID = GetImageIDFromName(context, "User_Guest.jpg"), Role = User.UserRole.Guest }//,
                //new User { Username = "BenjaminButton", EmailAddress = "benjamin.button@techstore.com", PasswordHash = "", Forename = "Benjamin", Surname = "Button", ProfilePictureID = GetImageIDFromName(context, "TechStoreLogo.png"), Role = "User" }
                );
            context.SaveChanges();
            Console.WriteLine("[#] Users updated.");


            context.Products.AddOrUpdate(
                p => p.Name,
                new Product { Name = "SONY BRAVIA XR-55A95LU 55\" Smart 4K Ultra HD HDR OLED TV", Description = "", Price = 2499.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Bravia_XR-55A95LU-01.jpg"), CategoryID = 1 },
                new Product { Name = "SONY BRAVIA XR-55A80LU 55\" Smart 4K Ultra HD HDR OLED TV", Description = "", Price = 1599.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Bravia_XR55A80LU-01.jpg"), CategoryID = 1 },
                new Product { Name = "Samsung S90C 55\" 4K OLED HDR Smart TV (2023)", Description = "", Price = 1129.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_S90C-01.png"), CategoryID = 1 },
                new Product { Name = "LG OLED evo C3 55\" 4K OLED HDR Smart TV (OLED55C34LA)", Description = "", Price = 1069.00M, DefaultImageID = GetImageIDFromName(context, "LG_OLED55C3-01.jpg"), CategoryID = 1 },
                new Product { Name = "LG OLED evo G3 55\" 4K OLED HDR Smart TV (OLED55G36LA)", Description = "", Price = 1399.00M, DefaultImageID = GetImageIDFromName(context, "LG-OLED55G36LA-01.jpg"), CategoryID = 1 },
                new Product { Name = "HISENSE 55\" Smart 4K Ultra HD HDR Mini-LED TV (55U8KQTUK)", Description = "", Price = 999.00M, DefaultImageID = GetImageIDFromName(context, "Hisense_55U8K-01.png"), CategoryID = 1 },
                new Product { Name = "Alienware 34\" WQHD 21:9 1800R QD OLED 165Hz Curved Gaming Monitor (AW3423DWF)", Description = "", Price = 740.33M, DefaultImageID = GetImageIDFromName(context, "Alienware_AW3423DWF-01.jpg"), CategoryID = 2 },
                new Product { Name = "AOC Gaming 34\" WQHD 144Hz Curved Monitor (CU34G2X)", Description = "", Price = 317.97M, DefaultImageID = GetImageIDFromName(context, "AOC_CU34G2-01.png"), CategoryID = 2 },
                new Product { Name = "Corsair Xeneon Flex 45\" OLED WQHD Bendable Display Gaming Monitor (45WQHD240 )", Description = "", Price = 1499.98M, DefaultImageID = GetImageIDFromName(context, "Corsair_Xeneon_Flex_45WQHD240-01.jpg"), CategoryID = 2 },
                new Product { Name = "LG UltraGear 21:9 UW-QHD 160Hz Nano IPS 37.5\" Curved Gaming Monitor (38GN950)", Description = "", Price = 899.98M, DefaultImageID = GetImageIDFromName(context, "LG_UltraGear_38GN950-01.jpg"), CategoryID = 2 },
                new Product { Name = "Samsung Odyssey Neo G9 49\" DQHD 32:9 Quantum Mini-LED Gaming Monitor", Description = "", Price = 1350.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Odyssey_Neo_G9-01.jpg"), CategoryID = 2 },
                new Product { Name = "Samsung Odyssey OLED G9 49\" 240Hz 32:9 Smart Gaming Monitor", Description = "", Price = 1399.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Odyssey_OLED_G9-01.png"), CategoryID = 2 },
                new Product { Name = "Sony Xperia 1 V", Description = "", Price = 1249.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Xperia_1_V-01.jpg"), CategoryID = 3 },
                new Product { Name = "Sony Xperia 5 V", Description = "", Price = 849.00M, DefaultImageID = GetImageIDFromName(context, "Sony_Xperia_5_V-01.jpg"), CategoryID = 3 },
                new Product { Name = "ROG Phone 7 Ultimate", Description = "", Price = 1199.99M, DefaultImageID = GetImageIDFromName(context, "Asus_ROG_Phone_7_Ultimate-01.jpg"), CategoryID = 3 },
                new Product { Name = "Samsung S4 Ultra 512GB", Description = "", Price = 1349.00M, DefaultImageID = GetImageIDFromName(context, "Samsung_Galaxy_S24_Ultra-01.jpg"), CategoryID = 3 },
                new Product { Name = "Google Pixel 8 Pro", Description = "", Price = 999.00M, DefaultImageID = GetImageIDFromName(context, "Google_Pixel_8_Pro-01.png"), CategoryID = 3 },
                new Product { Name = "Sony WH-1000XM5 Wireless Noise Cancelling Headphones", Description = "", Price = 299.00M, DefaultImageID = GetImageIDFromName(context, "Sony_WH-1000XM5-01.jpg"), CategoryID = 4 },
                new Product { Name = "Sony WH-1000XM4 Wireless Noise Cancelling Headphones", Description = "", Price = 249.00M, DefaultImageID = GetImageIDFromName(context, "Sony_WH-1000XM4-01.jpg"), CategoryID = 4 },
                new Product { Name = "Sennheiser MOMENTUM 4 Wireless Headphones", Description = "", Price = 249.99M, DefaultImageID = GetImageIDFromName(context, "Sennheiser_MOMENTUM_4-01.jpg"), CategoryID = 4 },
                new Product { Name = "Bose QuietComfort Ultra Wireless Noise Cancelling Headphones", Description = "", Price = 449.00M, DefaultImageID = GetImageIDFromName(context, "Bose_Quietcomfort_Ultra-Headphones-01.png"), CategoryID = 4 }
            );
            context.SaveChanges();
            Console.WriteLine("[#] Products updated.");


            Console.WriteLine("[#] Updating ProductImages...");
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

                    List<ProductImage> newProductImages = new List<ProductImage>();

                    foreach (Image pImage in pImages)
                    {
                        // Create a new ProductImage for each image linked to the that product
                        newProductImages.Add(new ProductImage { ProductID = product.ProductID, ImageID = pImage.ImageID });
                    }

                    // Filter out ProductImages already present in the database.
                    List<ProductImage> uniqueProductImages = newProductImages
                        .Where(newProductImage => !context.ProductImages.Any( // Iterate through each ProductImage in the list
                            existingProductImage => (existingProductImage.ImageID == newProductImage.ImageID) // Check against each database record
                                                 && (existingProductImage.ProductID == newProductImage.ProductID))) // This check might be redundant if an image is only ever linked to one product.
                        .ToList();

                    if (uniqueProductImages.Count > 0)
                    {
                        context.ProductImages.AddRange(uniqueProductImages);
                        Console.WriteLine($" - Added {uniqueProductImages.Count} images to {product.Name} matching \"{imageFormat}-#\" ");
                    }

                    else
                    {
                        //Console.WriteLine($"No new product-to-image links found for {product.Name} with the image format \"{imageFormat}-#\".");
                    }

                }

            }
            context.SaveChanges();
            Console.WriteLine("[#] ProductImages updated.");







        }
        
        public void ResetAllIDs(DatabaseContext context)
        {
            // Reset seed ID of columns with data to last record.
            // Console.WriteLine($"~~~ Resetting seed IDs for each table...");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Categories', RESEED, {context.Categories.Max(e => (int?)e.CategoryID) ?? 1})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Products', RESEED, {context.Products.Max(e => (int?)e.ProductID) ?? 1})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Users', RESEED, {context.Users.Max(e => (int?)e.UserID) ?? 1})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Images', RESEED, {context.Images.Max(e => (int?)e.ImageID) ?? 1})");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('ProductImages', RESEED, {context.ProductImages.Max(e => (int?)e.ProductImageID) ?? 1})");
        }



        public void ResetAllIDs_Temp(DatabaseContext context)
        /* On a new database - it's fine.
         * If you delete some records, it's fine.
         * If you delete all records in a table - nope! ID 2 - take it or leave it.
         */
        {

            // Check if the maximum UserID in the Users table is greater than 0
            Console.WriteLine("Checking if there are Users with IDs greater than 0: " + (context.Users.Max(e => (int?)e.UserID) > 0));

            // Check if the maximum ProductImageID in the ProductImages table is greater than 0
            Console.WriteLine("Checking if there are ProductImages with IDs greater than 0: " + (context.ProductImages.Max(e => (int?)e.ProductImageID) > 0));

            // Check if the maximum ProductID in the Products table is greater than 0
            Console.WriteLine("Checking if there are Products with IDs greater than 0: " + (context.Products.Max(e => (int?)e.ProductID) > 0));

            // Check if the maximum CategoryID in the Categories table is greater than 0
            Console.WriteLine("Checking if there are Categories with IDs greater than 0: " + (context.Categories.Max(e => (int?)e.CategoryID) > 0));

            // Check if the maximum ImageID in the Images table is greater than 0
            Console.WriteLine("Checking if there are Images with IDs greater than 0: " + (context.Images.Max(e => (int?)e.ImageID) > 0));




            // Wipe all data from empty tables, restarting the seed value

            if (!context.Users.ToList().Any() && (context.Users.Max(e => (int?)e.UserID) == null || context.Users.Max(e => (int?)e.UserID) == 0))
            {
                // No records found in "Users"
                Console.WriteLine("~~~ No records found in 'Users'. Rebuilding table Users");
                RebuildTable(context, "Users");
            }

            if (!context.ProductImages.ToList().Any() && (context.ProductImages.Max(e => (int?)e.ProductImageID) == null || context.ProductImages.Max(e => (int?)e.ProductImageID) == 0))
            {
                // No records found in "ProductImages"
                Console.WriteLine("~~~ No records found in 'ProductImages'. Rebuilding table ProductImages");
                RebuildTable(context, "ProductImages");
            }

            if (!context.Products.ToList().Any() && (context.Products.Max(e => (int?)e.ProductID) == null || context.Products.Max(e => (int?)e.ProductID) == 0))
            {
                // No records found in "Products"
                Console.WriteLine("~~~ No records found in 'Products'. Rebuilding table Products");
                RebuildTable(context, "Products");
            }

            if (!context.Categories.ToList().Any() && (context.Categories.Max(e => (int?)e.CategoryID) == null || context.Categories.Max(e => (int?)e.CategoryID) == 0))
            {
                // No records found in "Categories"
                Console.WriteLine("~~~ No records found in 'Categories'. Rebuilding table Categories");
                RebuildTable(context, "Categories");
            }

            if (!context.Images.ToList().Any() && (context.Images.Max(e => (int?)e.ImageID) == null || context.Images.Max(e => (int?)e.ImageID) == 0))
            {
                // No records found in "Images"
                Console.WriteLine("~~~ No records found in 'Images'. Rebuilding table Images");
                RebuildTable(context, "Images");
            }






            // Reset seed ID of columns with data to last record.
            Console.WriteLine($"~~~ Resetting seed IDs for each table...");

            Console.WriteLine($"~~~ Resetting seed ID for Categories");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Categories', RESEED, {context.Categories.Max(e => (int?)e.CategoryID) ?? 1})");

            Console.WriteLine($"~~~ Resetting seed ID for Products");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Products', RESEED, {context.Products.Max(e => (int?)e.ProductID) ?? 1})");

            Console.WriteLine($"~~~ Resetting seed ID for Users");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Users', RESEED, {context.Users.Max(e => (int?)e.UserID) ?? 1})");

            Console.WriteLine($"~~~ Resetting seed ID for Images");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Images', RESEED, {context.Images.Max(e => (int?)e.ImageID) ?? 1})");

            Console.WriteLine($"~~~ Resetting seed ID for ProductImages");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('ProductImages', RESEED, {context.ProductImages.Max(e => (int?)e.ProductImageID) ?? 1})");

        }

        public void RebuildTable(DatabaseContext db, string tableName)
        {
            Console.WriteLine("I'm trying!");
            //db.Database.ExecuteSqlCommand($"SELECT TOP 0 * INTO TempTable FROM {tableName}");
            db.Database.ExecuteSqlCommand($"DELETE FROM {tableName}");
            //db.Database.ExecuteSqlCommand($"EXEC sp_rename 'TempTable', '{tableName}'");
            //db.Database.ExecuteSqlCommand($"TRUNCATE TABLE {tableName}");
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('{tableName}', RESEED, 0)");
        }


        public int GetImageIDFromName(DatabaseContext context, string name)
        {
            int imageID = context.Images
                .Where(image => image.ImageName == name)
                .Select(image => image.ImageID)
                .FirstOrDefault();

            if (imageID == 0) { throw new Exception($"Call GetImageIDFromName(): The image file \"{name}\" could not be found. Check for typos in the name or file extension."); }

            return imageID;
        }





    }
}
