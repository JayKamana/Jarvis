namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsAndComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNumber = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Details = c.String(),
                        FRDId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.FRDId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Details = c.String(),
                        ItemId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ItemId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "FRDId", "dbo.FRDs");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ItemId", "dbo.Items");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "FRDId" });
            DropTable("dbo.Comments");
            DropTable("dbo.Items");
        }
    }
}
