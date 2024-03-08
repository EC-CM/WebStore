using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class UserLoginSession
    {
        [Key]
        public int UserLoginSessionID { get; set; }
        [Required(ErrorMessage = "A user must be linked.")]
        public int UserID { get; set; }
        public bool Active { get; set; }

 
        [ForeignKey("UserID")]
        public User User { get; set; }


        public UserLoginSession()
        {
            UserID = 0;
            Active = true;
        }

        public UserLoginSession(int userID, bool active = true)
        {
            UserID = userID;
            Active = active; ;
        }
    }
}

