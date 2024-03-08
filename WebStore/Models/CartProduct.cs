using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class CartProduct
    {
        [Key]
        public int CartProductID { get; set; }
        [Required(ErrorMessage = "A cart must be linked.")]
        public int UserCartID { get; set; }
        [Required(ErrorMessage = "A product must be linked.")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("UserCartID")]
        public UserCart Cart { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }


        public CartProduct()
        {
            UserCartID = 0;
            ProductID = 0;
            Quantity = 0;
        }

        public CartProduct(int userCartID, int productID, int quantity)
        {
            UserCartID = userCartID;
            ProductID = productID;
            Quantity = quantity;
        }

    }
}