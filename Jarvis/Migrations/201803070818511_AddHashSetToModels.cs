namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHashSetToModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SMSDemandFRDs", "FRDId", "dbo.FRDs");
            DropForeignKey("dbo.SMSDemandFRDs", "SMSDemandId", "dbo.SMSDemands");
            DropIndex("dbo.SMSDemandFRDs", new[] { "FRDId" });
            DropIndex("dbo.SMSDemandFRDs", new[] { "SMSDemandId" });

            DropTable("dbo.SMSDemandFRDs");

            CreateTable(
                "dbo.SMSDemandFRDs",
                c => new
                    {
                        SMSDemand_ID = c.Int(nullable: false),
                        FRD_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SMSDemand_ID, t.FRD_Id })
                .ForeignKey("dbo.SMSDemands", t => t.SMSDemand_ID, cascadeDelete: true)
                .ForeignKey("dbo.FRDs", t => t.FRD_Id, cascadeDelete: true)
                .Index(t => t.SMSDemand_ID)
                .Index(t => t.FRD_Id);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SMSDemandFRDs",
                c => new
                    {
                        FRDId = c.Int(nullable: false),
                        SMSDemandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FRDId, t.SMSDemandId });
            
            DropForeignKey("dbo.SMSDemandFRDs", "FRD_Id", "dbo.FRDs");
            DropForeignKey("dbo.SMSDemandFRDs", "SMSDemand_ID", "dbo.SMSDemands");
            DropIndex("dbo.SMSDemandFRDs", new[] { "FRD_Id" });
            DropIndex("dbo.SMSDemandFRDs", new[] { "SMSDemand_ID" });
            DropTable("dbo.SMSDemandFRDs");
            CreateIndex("dbo.SMSDemandFRDs", "SMSDemandId");
            CreateIndex("dbo.SMSDemandFRDs", "FRDId");
            AddForeignKey("dbo.SMSDemandFRDs", "SMSDemandId", "dbo.SMSDemands", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SMSDemandFRDs", "FRDId", "dbo.FRDs", "Id", cascadeDelete: true);
        }
    }
}
