namespace Jarvis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
        INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9c71c0a5-1a21-43db-802c-857e44caa398', N'admin@jarvis.com', 0, N'ADbgYna5CE+H6mx6Fta7RnPW1yJSmX3E93qHGf/KIK4aT2z74UJSa62dVZQ1vLQLFg==', N'3e4a7a0c-a15c-4a12-9ac8-0bad6c3fa30f', NULL, 0, 0, NULL, 1, 0, N'admin@jarvis.com')
        INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a4fbb4d3-66a2-4e7f-9fd7-e8730c96ba71', N'guest@jarvis.com', 0, N'ANaooLHGhD5vE5WIrADZpfsgqtrsPwlb1BdDRqkrlf6mL0pqEZuAu+uccf5owLR+vQ==', N'd4256e84-a31e-4cd1-9587-d048b25cc705', NULL, 0, 0, NULL, 1, 0, N'guest@jarvis.com')

        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3cdc4f2a-692d-423d-80e5-66413f6d4e93', N'CanManageFRD')

        INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9c71c0a5-1a21-43db-802c-857e44caa398', N'3cdc4f2a-692d-423d-80e5-66413f6d4e93')

");
        }
        
        public override void Down()
        {
        }
    }
}
