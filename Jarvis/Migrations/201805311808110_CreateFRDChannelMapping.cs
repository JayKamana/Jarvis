namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFRDChannelMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FRDChannelMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FRDId = c.Int(nullable: false),
                        ChannelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .Index(t => t.FRDId)
                .Index(t => t.ChannelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FRDChannelMappings", "FRDId", "dbo.FRDs");
            DropForeignKey("dbo.FRDChannelMappings", "ChannelId", "dbo.Channels");
            DropIndex("dbo.FRDChannelMappings", new[] { "ChannelId" });
            DropIndex("dbo.FRDChannelMappings", new[] { "FRDId" });
            DropTable("dbo.FRDChannelMappings");
        }
    }
}
