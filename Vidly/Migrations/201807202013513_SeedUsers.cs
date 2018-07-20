namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'627dd22b-0707-4458-b01a-85470cddd683', N'guest@vidly.com', 0, N'AMCnjZPgra1dxTriSNyJIPbDpQaUvKHVkLEjMnzkcrbiOV8R+Sk6FWGw/pBOYDHK1Q==', N'88bbfdbe-6bfd-4112-81fb-09df6c5e27a9', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'720ac0c0-8157-4d68-ac05-f3cb3708f809', N'admin@vidly.com', 0, N'AB5mpUNYNJW8r9j0x2WyX6SMxTjclhAwgOdCk8gwmv7XQErmIIE2urorvOkrpLMXDw==', N'40618ad2-71dd-4d28-8f6d-b1a0740f0b7b', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b84b294b-3d39-4baa-8ec1-e26285343546', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'720ac0c0-8157-4d68-ac05-f3cb3708f809', N'b84b294b-3d39-4baa-8ec1-e26285343546')

");
        }
        
        public override void Down()
        {
        }
    }
}
