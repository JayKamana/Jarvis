namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDistributionListsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DistributionLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DistributionLists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DistributionLists", new[] { "UserId" });
            DropTable("dbo.DistributionLists");
        }
    }
}
