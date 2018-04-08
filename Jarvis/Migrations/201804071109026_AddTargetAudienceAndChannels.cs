namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTargetAudienceAndChannels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TargetAudiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FRDChannels",
                c => new
                    {
                        FRD_Id = c.Int(nullable: false),
                        Channel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FRD_Id, t.Channel_Id })
                .ForeignKey("dbo.FRDs", t => t.FRD_Id, cascadeDelete: true)
                .ForeignKey("dbo.Channels", t => t.Channel_Id, cascadeDelete: true)
                .Index(t => t.FRD_Id)
                .Index(t => t.Channel_Id);
            
            CreateTable(
                "dbo.TargetAudienceFRDs",
                c => new
                    {
                        TargetAudience_Id = c.Int(nullable: false),
                        FRD_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TargetAudience_Id, t.FRD_Id })
                .ForeignKey("dbo.TargetAudiences", t => t.TargetAudience_Id, cascadeDelete: true)
                .ForeignKey("dbo.FRDs", t => t.FRD_Id, cascadeDelete: true)
                .Index(t => t.TargetAudience_Id)
                .Index(t => t.FRD_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TargetAudienceFRDs", "FRD_Id", "dbo.FRDs");
            DropForeignKey("dbo.TargetAudienceFRDs", "TargetAudience_Id", "dbo.TargetAudiences");
            DropForeignKey("dbo.FRDChannels", "Channel_Id", "dbo.Channels");
            DropForeignKey("dbo.FRDChannels", "FRD_Id", "dbo.FRDs");
            DropIndex("dbo.TargetAudienceFRDs", new[] { "FRD_Id" });
            DropIndex("dbo.TargetAudienceFRDs", new[] { "TargetAudience_Id" });
            DropIndex("dbo.FRDChannels", new[] { "Channel_Id" });
            DropIndex("dbo.FRDChannels", new[] { "FRD_Id" });
            DropTable("dbo.TargetAudienceFRDs");
            DropTable("dbo.FRDChannels");
            DropTable("dbo.TargetAudiences");
            DropTable("dbo.Channels");
        }
    }
}
