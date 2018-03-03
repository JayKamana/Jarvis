namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmsCodeAndDemand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SMSDemands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sender = c.Int(nullable: false),
                        Content_tr = c.String(nullable: false),
                        Content_en = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                        FRDId = c.Int(nullable: false),
                        SMSCodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .ForeignKey("dbo.SMSCodes", t => t.SMSCodeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FRDId)
                .Index(t => t.SMSCodeId);
            
            CreateTable(
                "dbo.SMSCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SMSDemands", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SMSDemands", "SMSCodeId", "dbo.SMSCodes");
            DropForeignKey("dbo.SMSDemands", "FRDId", "dbo.FRDs");
            DropIndex("dbo.SMSDemands", new[] { "SMSCodeId" });
            DropIndex("dbo.SMSDemands", new[] { "FRDId" });
            DropIndex("dbo.SMSDemands", new[] { "UserId" });
            DropTable("dbo.SMSCodes");
            DropTable("dbo.SMSDemands");
        }
    }
}
