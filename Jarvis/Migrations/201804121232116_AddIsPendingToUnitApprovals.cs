namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPendingToUnitApprovals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnitApprovals", "IsPending", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnitApprovals", "IsPending");
        }
    }
}
