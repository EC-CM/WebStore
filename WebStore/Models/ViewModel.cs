using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class ViewModel
    {
        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public ViewModel
        // Allows for any combination of models to be passed in
        (
            List<Category> categories = null,
            List<User> users = null,
            List<Product> products = null,
            List<Image> images = null,
            List<ProductImage> productImages = null)
        {
            Categories = categories ?? new List<Category>();
            Users = users ?? new List<User>();
            Products = products ?? new List<Product>();
            Images = images ?? new List<Image>();
            ProductImages = productImages ?? new List<ProductImage>();
        }
    }
}
