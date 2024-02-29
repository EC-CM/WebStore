using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Diagnostics;

/* TO DO:
 * 
 * - Validation of image properties (size, format)?
 * - Constructor: Handle UnauthorizedAccessException (Access Denied) and IOException (File in use/locked by another process).
 * - Constructor: Replace null data with placeholder data?
 */

namespace WebStore.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }

        [Required(ErrorMessage = "The image must have a name.")]
        [StringLength(100)]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "ImageData is required.")]
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] ImageData { get; set; }

        public string ImagePath { get; set; }


        public Image()
        // Parameter-less constructor for functionality
        {
            ImagePath = "";
        }

        public Image(string imagePath)
        {
            // Check if the image file exists
            if (File.Exists(imagePath))
            {
                ImageName = Path.GetFileName(imagePath);
                ImageData = GetImageData(imagePath);
                ImagePath = imagePath;
            }
            else
            {
                Debug.WriteLine($"File not found: {imagePath}");
                // All attributes are null (ImageName, ImageData, ImagePath)
            }

        }

        private byte[] GetImageData(string imagePath)
        // Convert an image file into a byte array
        {
            try
            {
                // Open the image file and read the raw data
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (MemoryStream ms = new MemoryStream())
                {
                    // Copy the raw data into a MemoryStream and convert into a byte array
                    fs.CopyTo(ms);
                    return ms.ToArray();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing image from {imagePath}: {ex.Message}");
                return null; // ImageData attribute is null
            }
        }
    }



}
