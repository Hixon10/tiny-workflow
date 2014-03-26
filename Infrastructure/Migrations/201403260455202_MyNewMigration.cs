namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyNewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ApplicationId = c.Guid(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        ApplicationLogStatusId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationLogStatus", t => t.ApplicationLogStatusId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationId)
                .Index(t => t.ApplicationLogStatusId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestedMoney = c.Double(nullable: false),
                        ProductCategoryId = c.Guid(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Priority = c.Int(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationLogStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationLogs", "ApplicationUserId", "dbo.IdentityUsers");
            DropForeignKey("dbo.ApplicationLogs", "ApplicationLogStatusId", "dbo.ApplicationLogStatus");
            DropForeignKey("dbo.ApplicationLogs", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Applications", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Applications", "ApplicationUserId", "dbo.IdentityUsers");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.IdentityUsers");
            DropForeignKey("dbo.IdentityUserRoles", "RoleId", "dbo.IdentityRoles");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.IdentityUserClaims", "User_Id", "dbo.IdentityUsers");
            DropIndex("dbo.ApplicationLogs", new[] { "ApplicationUserId" });
            DropIndex("dbo.ApplicationLogs", new[] { "ApplicationLogStatusId" });
            DropIndex("dbo.ApplicationLogs", new[] { "ApplicationId" });
            DropIndex("dbo.Applications", new[] { "ProductCategoryId" });
            DropIndex("dbo.Applications", new[] { "ApplicationUserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "RoleId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "User_Id" });
            DropTable("dbo.ApplicationLogStatus");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.Applications");
            DropTable("dbo.ApplicationLogs");
            DropTable("dbo.Accounts");
        }
    }
}
