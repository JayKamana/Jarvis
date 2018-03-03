namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToSmsDemand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMSDemands", "UpdateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMSDemands", "UpdateDate");
        }
    }
}
