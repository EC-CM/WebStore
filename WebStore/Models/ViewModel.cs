using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class ViewModel
    {
        // Is a single element property needed?
        // Maybe a list of strings also for extra data?

        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public Image Image { get; set; }
        public ProductImage ProductImage { get; set; }

        public String SearchPhrase { get; set; }

        public ViewModel
        // Allows for any combination of models to be passed in
        (
            List<Category> categories = null,
            List<User> users = null,
            List<Product> products = null,
            List<Image> images = null,
            List<ProductImage> productImages = null,

            Category category = null,
            User user = null,
            Product product = null,
            Image image = null,
            ProductImage productImage = null,
            
            string searchPhrase = "")
        {
            Categories = categories ?? new List<Category>();
            Users = users ?? new List<User>();
            Products = products ?? new List<Product>();
            Images = images ?? new List<Image>();
            ProductImages = productImages ?? new List<ProductImage>();

            Category = category ?? new Category();
            User = user ?? new User();
            Product = product ?? new Product();
            Image = image ?? new Image();
            ProductImage = productImage ?? new ProductImage();

            SearchPhrase = searchPhrase;
        }
    }
}
