namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartProducts",
                c => new
                    {
                        CartProductID = c.Int(nullable: false, identity: true),
                        UserCartID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartProductID)
                .ForeignKey("dbo.UserCarts", t => t.UserCartID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.UserCartID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.UserCarts",
                c => new
                    {
                        UserCartID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserCartID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
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
                "dbo.UserLoginSessions",
                c => new
                    {
                        UserLoginSessionID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserLoginSessionID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLoginSessions", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserListItems", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserListItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ImageID", "dbo.Images");
            DropForeignKey("dbo.CartProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ImageID", "dbo.Images");
            DropForeignKey("dbo.UserCarts", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "ProfilePictureID", "dbo.Images");
            DropForeignKey("dbo.CartProducts", "UserCartID", "dbo.UserCarts");
            DropIndex("dbo.UserLoginSessions", new[] { "UserID" });
            DropIndex("dbo.UserListItems", new[] { "ProductID" });
            DropIndex("dbo.UserListItems", new[] { "UserID" });
            DropIndex("dbo.ProductImages", new[] { "ImageID" });
            DropIndex("dbo.ProductImages", new[] { "ProductID" });
            DropIndex("dbo.Categories", new[] { "ImageID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Users", new[] { "ProfilePictureID" });
            DropIndex("dbo.UserCarts", new[] { "UserID" });
            DropIndex("dbo.CartProducts", new[] { "ProductID" });
            DropIndex("dbo.CartProducts", new[] { "UserCartID" });
            DropTable("dbo.UserLoginSessions");
            DropTable("dbo.UserListItems");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Images");
            DropTable("dbo.Users");
            DropTable("dbo.UserCarts");
            DropTable("dbo.CartProducts");
        }
    }
}
