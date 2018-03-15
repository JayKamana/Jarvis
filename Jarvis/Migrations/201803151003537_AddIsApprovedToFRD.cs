namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsApprovedToFRD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FRDs", "isApproved", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FRDs", "isApproved");
        }
    }
}
