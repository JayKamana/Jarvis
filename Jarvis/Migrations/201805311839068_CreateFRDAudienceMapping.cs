namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFRDAudienceMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FRDAudienceMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FRDId = c.Int(nullable: false),
                        TargetAudienceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .ForeignKey("dbo.TargetAudiences", t => t.TargetAudienceId, cascadeDelete: true)
                .Index(t => t.FRDId)
                .Index(t => t.TargetAudienceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FRDAudienceMappings", "TargetAudienceId", "dbo.TargetAudiences");
            DropForeignKey("dbo.FRDAudienceMappings", "FRDId", "dbo.FRDs");
            DropIndex("dbo.FRDAudienceMappings", new[] { "TargetAudienceId" });
            DropIndex("dbo.FRDAudienceMappings", new[] { "FRDId" });
            DropTable("dbo.FRDAudienceMappings");
        }
    }
}
