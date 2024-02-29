using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    // Either this or a ProductID in images set to null for others
    public class ProductImage
    {
        [Key]
        public int ProductImageID { get; set; }


        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }


        [Required]
        public int ImageID { get; set; }
        [ForeignKey("ImageID")]
        public Image Image { get; set; }


        public ProductImage() { }

        public ProductImage(int productImageID, int productID, int imageID)
        {
            ProductImageID = productImageID;
            ProductID = productID;
            ImageID = imageID;
        }
    }
}