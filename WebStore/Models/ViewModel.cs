using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebStore.Models
{
    public class ViewModel
    {
        public enum SortBy
        {
            Default,
            Price,
            Name
        }

        public enum SortOrder
        {
            Ascending,
            Descending
        }

        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<UserListItem> UserListItems { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public Image Image { get; set; }
        public ProductImage ProductImage { get; set; }

        public String SearchPhrase { get; set; }

        public SortBy Sort { get; set; }
        public SortOrder Order { get; set; }


        public ViewModel
        // Allows for any combination of models to be passed in
        (
            List<Category> categories = null,
            List<User> users = null,
            List<Product> products = null,
            List<Image> images = null,
            List<ProductImage> productImages = null,
            List<UserListItem> userListItems = null,

            Category category = null,
            User user = null,
            Product product = null,
            Image image = null,
            ProductImage productImage = null,
            
            string searchPhrase = "",
            
            SortBy sort = SortBy.Default,
            SortOrder order = SortOrder.Ascending)
        {
            Categories = categories ?? new List<Category>();
            Users = users ?? new List<User>();
            Products = products ?? new List<Product>();
            Images = images ?? new List<Image>();
            ProductImages = productImages ?? new List<ProductImage>();
            UserListItems = userListItems ?? new List<UserListItem>();

            Category = category ?? new Category();
            User = user ?? new User();
            Product = product ?? new Product();
            Image = image ?? new Image();
            ProductImage = productImage ?? new ProductImage();

            SearchPhrase = searchPhrase;

            Sort = sort;
            Order = order;
        }
    }
}