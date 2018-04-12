namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnitApprovalsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "CanApproveFRD", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "CanApproveFRD");
        }
    }
}
