using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ImageID { get; set; }
        [ForeignKey("ImageID")]
        public Image Image { get; set; }

        public Category()
        {
            CategoryID = 0;
            Name = "";
            Description = "";
            ImageID = null;
        }

        public Category(int categoryID, string name, string description, int? imageID)
        {
            CategoryID = categoryID;
            Name = name;
            Description = description;
            ImageID = imageID;
        }
    }


}