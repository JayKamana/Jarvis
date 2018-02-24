namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddASPUserForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FRDs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.FRDs", "UserId");
            AddForeignKey("dbo.FRDs", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FRDs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FRDs", new[] { "UserId" });
            DropColumn("dbo.FRDs", "UserId");
        }
    }
}
