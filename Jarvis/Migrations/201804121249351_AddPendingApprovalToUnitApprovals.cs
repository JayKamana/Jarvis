namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPendingApprovalToUnitApprovals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnitApprovals", "PendingApproval", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnitApprovals", "IsPending", c => c.Boolean(nullable: false));
        }
    }
}
