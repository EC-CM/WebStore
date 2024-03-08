using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    // To have multiple lists, a link table storing user-list relationships would be needed.
    // In this case, a user is linked to items instead of to a list then items.

    public class UserListItem
    {
        [Key]
        public int UserListItemID { get; set; }

        [Required(ErrorMessage = ("A user must be linked to the list item."))]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required(ErrorMessage = ("A product must be linked to the list item."))]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        [StringLength(50, ErrorMessage = ("Character limit exceeded (50)."))]
        public string Notes { get; set; }

        public DateTime Timestamp { get; set; }

        public UserListItem()
        {
            UserID = 0;
            ProductID = 0;
            Notes = "";
            Timestamp = DateTime.Now;
        }

        public UserListItem(int userID, int productID, string notes = "")
        {
            UserID = userID;
            ProductID = productID;
            Notes = notes;
            Timestamp = DateTime.Now;
        }

    }
}