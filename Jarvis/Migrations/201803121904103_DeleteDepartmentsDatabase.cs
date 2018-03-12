namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDepartmentsDatabase : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Departments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
        }
    }
}
