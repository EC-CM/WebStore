namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(nullable: false, maxLength: 100),
                        ImageData = c.Binary(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImageID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultImageID = c.Int(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.UserListItems",
                c => new
                    {
                        UserListItemID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Notes = c.String(maxLength: 50),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserListItemID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        EmailAddress = c.String(),
                        PasswordHash = c.String(),
                        Forename = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Role = c.Int(nullable: false),
                        ProfilePictureID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Images", t => t.ProfilePictureID)
                .Index(t => t.ProfilePictureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserListItems", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "ProfilePictureID", "dbo.Images");
            DropForeignKey("dbo.UserListItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.ProductImages", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Categories", "ImageID", "dbo.Images");
            DropIndex("dbo.Users", new[] { "ProfilePictureID" });
            DropIndex("dbo.UserListItems", new[] { "ProductID" });
            DropIndex("dbo.UserListItems", new[] { "UserID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.ProductImages", new[] { "ImageID" });
            DropIndex("dbo.ProductImages", new[] { "ProductID" });
            DropIndex("dbo.Categories", new[] { "ImageID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserListItems");
            DropTable("dbo.Products");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Images");
            DropTable("dbo.Categories");
        }
    }
}
