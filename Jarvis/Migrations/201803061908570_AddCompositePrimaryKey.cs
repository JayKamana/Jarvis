namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompositePrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SMSDemandFRDs");
            AddPrimaryKey("dbo.SMSDemandFRDs", new[] { "FRDId", "SMSDemandId" });
            DropColumn("dbo.SMSDemandFRDs", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SMSDemandFRDs", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.SMSDemandFRDs");
            AddPrimaryKey("dbo.SMSDemandFRDs", "ID");
        }
    }
}
