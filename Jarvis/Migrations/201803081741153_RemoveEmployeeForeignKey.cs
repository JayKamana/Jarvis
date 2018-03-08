namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmployeeForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FRDs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.FRDs", new[] { "EmployeeId" });
            DropColumn("dbo.FRDs", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FRDs", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.FRDs", "EmployeeId");
            AddForeignKey("dbo.FRDs", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
