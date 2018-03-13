namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMangersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Departments", "ManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "ManagerId");
            AddForeignKey("dbo.Departments", "ManagerId", "dbo.Managers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Managers");
            DropIndex("dbo.Managers", new[] { "UserId" });
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropColumn("dbo.Departments", "ManagerId");
            DropTable("dbo.Managers");
        }
    }
}
