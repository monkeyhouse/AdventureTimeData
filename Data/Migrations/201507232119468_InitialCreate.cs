namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 500),
                        ParentID = c.Int(nullable: false),
                        ChildID = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedByID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedByID = c.String(maxLength: 128),
                        StoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pages", t => t.ParentID)
                .ForeignKey("dbo.Pages", t => t.ChildID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedByID)
                .Index(t => t.ParentID)
                .Index(t => t.ChildID)
                .Index(t => t.CreatedByID)
                .Index(t => t.ModifiedByID)
                .Index(t => t.StoryID, name: "IX_Story_ID");
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 3000),
                        PageNumber = c.Int(nullable: false),
                        IsEnding = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedByID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedByID = c.String(maxLength: 128),
                        StoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedByID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .Index(t => t.PageNumber, unique: true)
                .Index(t => t.CreatedByID)
                .Index(t => t.ModifiedByID)
                .Index(t => t.StoryID, name: "IX_Story_ID");
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Summary = c.String(nullable: false, maxLength: 500),
                        Settings = c.String(maxLength: 250),
                        IsLocked = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedByID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedByID = c.String(maxLength: 128),
                        FirstPageID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.Pages", t => t.FirstPageID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedByID)
                .Index(t => t.CreatedByID)
                .Index(t => t.ModifiedByID)
                .Index(t => t.FirstPageID, name: "IX_FirstPage_ID");
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 50),
                        IsApproved = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedByID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedByID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedByID)
                .Index(t => t.CreatedByID)
                .Index(t => t.ModifiedByID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StoryGenres",
                c => new
                    {
                        StoryID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryID, t.GenreID })
                .ForeignKey("dbo.Stories", t => t.StoryID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.StoryID, name: "IX_Story_ID")
                .Index(t => t.GenreID, name: "IX_Genre_ID");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Actions", "ModifiedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pages", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Stories", "ModifiedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoryGenres", "GenreID", "dbo.Genres");
            DropForeignKey("dbo.StoryGenres", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Genres", "ModifiedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Genres", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stories", "FirstPageID", "dbo.Pages");
            DropForeignKey("dbo.Stories", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Pages", "ModifiedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ChildID", "dbo.Pages");
            DropForeignKey("dbo.Pages", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ParentID", "dbo.Pages");
            DropIndex("dbo.StoryGenres", "IX_Genre_ID");
            DropIndex("dbo.StoryGenres", "IX_Story_ID");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Genres", new[] { "ModifiedByID" });
            DropIndex("dbo.Genres", new[] { "CreatedByID" });
            DropIndex("dbo.Stories", "IX_FirstPage_ID");
            DropIndex("dbo.Stories", new[] { "ModifiedByID" });
            DropIndex("dbo.Stories", new[] { "CreatedByID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pages", "IX_Story_ID");
            DropIndex("dbo.Pages", new[] { "ModifiedByID" });
            DropIndex("dbo.Pages", new[] { "CreatedByID" });
            DropIndex("dbo.Pages", new[] { "PageNumber" });
            DropIndex("dbo.Actions", "IX_Story_ID");
            DropIndex("dbo.Actions", new[] { "ModifiedByID" });
            DropIndex("dbo.Actions", new[] { "CreatedByID" });
            DropIndex("dbo.Actions", new[] { "ChildID" });
            DropIndex("dbo.Actions", new[] { "ParentID" });
            DropTable("dbo.StoryGenres");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Genres");
            DropTable("dbo.Stories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pages");
            DropTable("dbo.Actions");
        }
    }
}
