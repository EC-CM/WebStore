using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;


namespace WebStore.Models
{
    public class Product
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Product must be named.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must a positive value.")]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }


        public Product()
        {
            ID = 0;
            Name = "No Product Name";
            Description = "No Description";
            Price = 0;
            CategoryID = 0;
        }

        public Product(int id, string name, string description, decimal price, int categoryID)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryID = categoryID;
        }
    }
}