namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditDepartmentsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.AspNetUsers");
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropColumn("dbo.Departments", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "ManagerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Departments", "ManagerId");
            AddForeignKey("dbo.Departments", "ManagerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
