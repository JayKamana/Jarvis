namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChannelNumberAndTargetNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FRDChannelMappings", "ChannelNumber", c => c.Int(nullable: false));
            AddColumn("dbo.FRDAudienceMappings", "AudienceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FRDAudienceMappings", "AudienceNumber");
            DropColumn("dbo.FRDChannelMappings", "ChannelNumber");
        }
    }
}
