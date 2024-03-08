using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class UserCart
    {
        [Key]
        public int UserCartID { get; set; }
        [Required(ErrorMessage = "A cart must be linked.")]
        public int UserID { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public decimal TotalPrice => CalculateTotalPrice();

        [ForeignKey("UserID")]
        public User User { get; set; }

        public List<CartProduct> CartProducts { get; set; }

        public UserCart()
        {
            UserID = 0;
            Active = true;
            CartProducts = new List<CartProduct>();
        }

        public UserCart(int userID, bool active = true)
        {
            UserID = userID;
            Active = active;;
            CartProducts = new List<CartProduct>();

        }

        public decimal CalculateTotalPrice()
        {
            return CartProducts.Sum(p => p.Product.Price);
        }
    }
}