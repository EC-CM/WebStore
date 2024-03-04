using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Services
{
    public class ImageService : IDisposable
    {
        private DatabaseContext _db;

        public ImageService()
        {
            _db = new DatabaseContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// Retrieve an array of image file paths at a specified directory.
        /// </summary>
        public string[] GetImageFilePaths(string directoryPath, bool searchSubfolders = true)
        {
            
            // Expand a given relative file path into an absolute file path.
            directoryPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", directoryPath));

            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine($"[#] Local image directory {directoryPath} found.");
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
                Console.WriteLine($"Error: Directory {directoryPath} could not be found.");
                return null;
            }
        }

        /// <summary>
        /// Update the database with images from a local directory, converted into binary byte data.
        /// </summary>
        public void SeedImages()

        // Should this be in the image controller?
        {
            string imageDirectory = "Images";
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

                if (uniqueImages.Count > 0)
                {
                    _db.Images.AddRange(uniqueImages);
                    

                    // Print the names of each added image
                    foreach (Image addedImage in uniqueImages)
                    {
                        Console.WriteLine($"    - {addedImage.ImageName}");
                    }

                    _db.SaveChanges();
                    Console.WriteLine($"[#] Images seeded successfully! {uniqueImages.Count} new images were added to the database.");
                }
                else
                {
                    Console.WriteLine("Error: No new images found.");
                }
            }

            // Error message
            else if (imageFilePaths == null)
            {
                Debug.WriteLine($"Error: The image directory path \"{imageDirectory}\" was not found.");
            }
            else
            {
                Console.WriteLine($"Error: No images were found in {imageDirectory}");
            }
            //Image dog1 = new Image { ImageName = "Dog1", ImageData = GetImageData("~/Images/") };
        }
    }
}