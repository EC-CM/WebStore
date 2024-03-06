using System;
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
using System.Data.SqlTypes;
using System.Runtime.Remoting.Contexts;
using System.Collections.Generic;

namespace WebStore.Controllers
{
    public class ImagesController : Controller
    {
        private DatabaseContext _db;

        public ImagesController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        public ActionResult Index()
        // NOT IMPLEMENTED
        {
            return View();
        }

        /// <summary>
        /// Retrieve an image from the database based on its ID and optionally resize.
        /// </summary>
        public ActionResult GetImage(int? id = null, string image_name = "", int? height = null, int? width = null, Image imported_image = null)
        {
            Image image;

            if (id.HasValue || image_name != "")
            {
                if (id == null)
                { // image_name must have been provided
                    image = _db.Images.FirstOrDefault(i => i.ImageName == image_name);
                }
                else
                { // Either both id and image_name were provided or just id.
                    image = _db.Images.FirstOrDefault(i => i.ImageID == id);
                }
            }
            else if (imported_image != null)
            { // NOTE: Order matters here - when first, somehow an ID and Image are passed in and it errors?
                image = imported_image;
            }
            else
            {
                throw new Exception("An id or image_name was not provided to GetImage().");
            }


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


        public ActionResult GetUserProfileImage(int userID)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserID == userID);
            Image profileImage = _db.Images.FirstOrDefault(i => i.ImageID == user.ProfilePictureID);
            //return File(profileImage.ImageData, "image/jpeg");

            if (profileImage != null) 
            {
                return GetImage(imported_image: profileImage, height: 50); 
            }
            else { return Content("Profile image not found."); }
        }



        // ---------------------------------------------------------------------------------------------------------------------------
        // Redundant

        /// <summary>
        /// Retrieve an array of image file paths at a specified directory.
        /// </summary>
        public string[] GetImageFilePaths(string directoryPath, bool searchSubfolders = true)
        {
            // Expand a given relative file path into an absolute file path.
            directoryPath = Server.MapPath(directoryPath);

            if (Directory.Exists(directoryPath))
            {
                Debug.WriteLine($"Local image directory {directoryPath} found.");
                string[] imageFileExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                // Defines whether to check subfolders for images or only the directory specified
                SearchOption searchOption = searchSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                // Generate an array of all file paths for images in the specified directory (and subdirectories if specified)
                // Checking only for images (extensions are case-insensitive)
                string[] imagePaths = Directory.EnumerateFiles(directoryPath, "*.*", searchOption)
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

        // Should this be in the image controller?
        {
            // Temp
            _db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('Images', RESEED, {(_db.Images.Max(i => (int?)i.ImageID) ?? 0)})");


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