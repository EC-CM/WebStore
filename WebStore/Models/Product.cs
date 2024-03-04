using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product must be named.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must a positive value.")]
        public decimal Price { get; set; }
        public int? DefaultImageID { get; set; }

        
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }


        public Product()
        {
            Name = "No Product Name";
            Description = "No Description";
            Price = 0;
            DefaultImageID = null;
            CategoryID = 1;
        }

        public Product(int productID, string name, string description, decimal price, int defaultImageID, int categoryID)
        {
            ProductID = productID;
            Name = name;
            Description = description;
            Price = price;
            DefaultImageID = defaultImageID;
            CategoryID = categoryID;
        }
    }
}