namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetIsPendingToNotNull : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UnitApprovals", "IsPending");
        }
        
        public override void Down()
        {

        }
    }
}
