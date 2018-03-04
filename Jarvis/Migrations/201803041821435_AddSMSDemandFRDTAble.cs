namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSMSDemandFRDTAble : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SMSDemands", "FRDId", "dbo.FRDs");
            DropIndex("dbo.SMSDemands", new[] { "FRDId" });
            CreateTable(
                "dbo.SMSDemandFRDs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FRDId = c.Int(nullable: false),
                        SMSDemandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .ForeignKey("dbo.SMSDemands", t => t.SMSDemandId, cascadeDelete: true)
                .Index(t => t.FRDId)
                .Index(t => t.SMSDemandId);
            
            AddColumn("dbo.SMSCodes", "ReferenceCode", c => c.Int(nullable: false));
            DropColumn("dbo.SMSDemands", "FRDId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SMSDemands", "FRDId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SMSDemandFRDs", "SMSDemandId", "dbo.SMSDemands");
            DropForeignKey("dbo.SMSDemandFRDs", "FRDId", "dbo.FRDs");
            DropIndex("dbo.SMSDemandFRDs", new[] { "SMSDemandId" });
            DropIndex("dbo.SMSDemandFRDs", new[] { "FRDId" });
            DropColumn("dbo.SMSCodes", "ReferenceCode");
            DropTable("dbo.SMSDemandFRDs");
            CreateIndex("dbo.SMSDemands", "FRDId");
            AddForeignKey("dbo.SMSDemands", "FRDId", "dbo.FRDs", "Id", cascadeDelete: true);
        }
    }
}
