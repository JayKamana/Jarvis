namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSenderTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SMSDemands", "Sender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SMSDemands", "Sender", c => c.Int(nullable: false));
        }
    }
}
