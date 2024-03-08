namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carts : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.UserCarts", "UserID", "dbo.Users");
            DropForeignKey("dbo.CartProducts", "UserCartID", "dbo.UserCarts");
            DropIndex("dbo.UserCarts", new[] { "UserID" });
            DropIndex("dbo.CartProducts", new[] { "ProductID" });
            DropIndex("dbo.CartProducts", new[] { "UserCartID" });
            DropTable("dbo.UserCarts");
            DropTable("dbo.CartProducts");
        }
    }
}
