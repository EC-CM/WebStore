using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using System.IO;
using Size = System.Drawing.Size;
using Img = System.Drawing.Image;
using Bitmap = System.Drawing.Bitmap;
using WebStore.Models;
using System.Diagnostics;

using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

/* TO DO:
 * - Implement cropping image behaviour?
 * - More comments
 * - GetImage: The IF can come before the USINGs for resizing.
 */

/* NOTES:
 * Resource Usage: Creating a new instance of ApplicationDbContext for each request might result in increased resource usage. The context is typically designed to be reused across multiple requests, and managing its lifecycle carefully can be beneficial for performance.
 * Connection Pooling: If your ApplicationDbContext is configured to use a database connection, creating a new instance for each request might impact connection pooling. Connection pooling is an optimization technique where connections are reused rather than closed and reopened for each request.
 * Testing: Creating a new instance directly within the constructor can make it challenging to mock or substitute the ApplicationDbContext during unit testing. If unit testing is a concern, you might want to consider dependency injection and constructor injection for better testability.
 */

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _db;

        public HomeController()
        {
           _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        public ActionResult Index()
        {
            //var images = _db.Images.ToList();
            //return View("Index", images);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieve an image from the database based on its ID and optionally resize.
        /// </summary>
        public ActionResult GetImage(int id, int? height = null, int? width = null)
        {
            var image = _db.Images.FirstOrDefault(i => i.ImageID == id);
            if (image != null)
            {
                string imageType = Path.GetExtension(image.ImageName)
                    .TrimStart('.')
                    .ToLower();

                using (MemoryStream ms = new MemoryStream(image.ImageData))
                using (Img originalImage = Img.FromStream(ms))
                {

                    if (height.HasValue || width.HasValue)
                    // Resize image
                    {
                        if (width == null)
                        {
                            // Calculate scaled width
                            width = (int)(originalImage.Width * (height / (float)originalImage.Height));
                        }
                        else if (height == null)
                        {
                            // Calculate scaled height
                            height = (int)(originalImage.Height * (width / (float)originalImage.Width));
                        }

                        using (Img resizedImage = new Bitmap(originalImage, new Size(width.Value, height.Value)))
                        using (MemoryStream resizedMs = new MemoryStream())
                        {
                            resizedImage.Save(resizedMs, originalImage.RawFormat);
                            return File(resizedMs.ToArray(), $"image/{imageType}");
                        }

                    }
                    else
                    // No resizing needed
                    {
                        return File(image.ImageData, $"image/{imageType}");
                    }

                }

            }
            else
            // 404: Not Found
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Retrieve an array of image file paths at a specified directory.
        /// </summary>
        public string[] GetImageFilePaths(string directoryPath)
        {
            // Expand a given relative file path into an absolute file path.
            directoryPath = Server.MapPath(directoryPath);

            if (Directory.Exists(directoryPath))
            {
                Debug.WriteLine($"Local image directory {directoryPath} found.");
                string[] imageFileExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                // Generate an array of all image file paths in the specified directory
                // Checking only for images (extensions are case-insensitive)
                string[] imagePaths = Directory.EnumerateFiles(directoryPath)
                    .Where(file => imageFileExtensions.Contains(Path.GetExtension(file),
                                                                StringComparer.OrdinalIgnoreCase))
                    .ToArray(); // Convert from IEnumerable<string>

                return imagePaths;
            }
            else
            {
                Debug.WriteLine($"Error: Directory {directoryPath} could not be found.");
                return null;
            }
        }

        /// <summary>
        /// Update the database with images from a local directory, converted into binary byte data.
        /// </summary>
        public ActionResult SeedImages()
        {
            string imageDirectory = "~/Images/";
            string[] imageFilePaths = GetImageFilePaths(imageDirectory);

            if (imageFilePaths != null && imageFilePaths.Length > 0)
            // Directory exists and images were found
            {
                List<Image> imagesFound = imageFilePaths.Select(path => new Image(path))
                    .Where(img => img.ImageData != null)
                    .ToList();

                // Filter out images already present in the database.
                List<Image> uniqueImages = imagesFound
                    .Where(newImage => !_db.Images.Any( // Iterate through each image in the list
                        existingImage => existingImage.ImageName == newImage.ImageName)) // Check against each database record
                    .ToList();

                _db.Images.AddRange(uniqueImages);
                _db.SaveChanges();

                // Success message
                return Content($"Images seeded successfully! {uniqueImages.Count} new images were added to the database.");
            }

            // Error message
            if (imageFilePaths == null)
            {
                return Content($"Error: The image directory path \"{imageDirectory}\" was not found.");
            }
            else
            {
                return Content($"Error: No images were found in {imageDirectory}");
            }

            //Image dog1 = new Image { ImageName = "Dog1", ImageData = GetImageData("~/Images/") };
        }
    }
}