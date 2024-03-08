using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls;

namespace WebStore.Models
{
    public class User
    {
        public enum UserRole
        {
            Admin,
            User,
            Guest
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string Forename { get; set; }
        public string Surname {  get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public UserRole Role { get; set; }

        public int? ProfilePictureID { get; set; }    // RENAME TO ProfileImage
        [ForeignKey("ProfilePictureID")]
        public Image ProfileImage { get; set; }

        public User() 
        {
            UserID = 0;
            Username = "Guest";
            EmailAddress = "";
            PasswordHash = "";
            Forename = "Guest";
            Surname = "";
            Role = UserRole.Guest;
            ProfilePictureID = null;
        }

        public User(int userID, string username, string emailAddress, string passwordHash, string forename, string surname, int? profilePictureID=null, UserRole role=UserRole.User)
        {
            UserID = userID;
            Username = username;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            Forename = forename;
            Surname = surname;
            Role = role;
            ProfilePictureID = profilePictureID;
        }
    }
}