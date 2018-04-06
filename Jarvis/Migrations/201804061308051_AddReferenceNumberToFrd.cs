namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferenceNumberToFrd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FRDs", "ReferenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FRDs", "ReferenceNumber");
        }
    }
}
