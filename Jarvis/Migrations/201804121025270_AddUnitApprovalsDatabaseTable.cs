namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnitApprovalsDatabaseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnitApprovals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsApproved = c.Boolean(nullable: false),
                        ApprovalDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        FRDId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.FRDs", t => t.FRDId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.FRDId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnitApprovals", "FRDId", "dbo.FRDs");
            DropForeignKey("dbo.UnitApprovals", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.UnitApprovals", new[] { "FRDId" });
            DropIndex("dbo.UnitApprovals", new[] { "DepartmentId" });
            DropTable("dbo.UnitApprovals");
        }
    }
}
