namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationDateToFrd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FRDs", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FRDs", "CreationDate");
        }
    }
}
