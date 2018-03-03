namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredOnSmsDemandContents : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SMSDemands", "Content_tr", c => c.String());
            AlterColumn("dbo.SMSDemands", "Content_en", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SMSDemands", "Content_en", c => c.String(nullable: false));
            AlterColumn("dbo.SMSDemands", "Content_tr", c => c.String(nullable: false));
        }
    }
}
