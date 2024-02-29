using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebStore.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string Forename { get; set; }
        public string Surname {  get; set; }
        //public string PhoneNumber { get; set; }
        //public string Address { get; set; }

        public int? ProfilePictureID { get; set; }    // RENAME TO ProfileImage
        [ForeignKey("ProfilePictureID")]
        public Image ProfileImage { get; set; }

        public User() 
        {
            Username = "";
            EmailAddress = "";
            PasswordHash = "";
            Forename = "";
            Surname = "";
            ProfilePictureID = null;
        }

        public User(int userID, string username, string emailAddress, string passwordHash, string forename, string surname, int? profilePictureID)
        {
            UserID = userID;
            Username = username;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            Forename = forename;
            Surname = surname;
            ProfilePictureID = profilePictureID;
        }
    }
}