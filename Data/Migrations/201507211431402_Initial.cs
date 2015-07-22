namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 500),
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Segments", t => t.ParentId, cascadeDelete: true)
                .ForeignKey("dbo.Segments", t => t.ChildId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Segments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 3000),
                        IsEnding = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
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
                "dbo.Stories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Byline = c.String(),
                        IsLocked = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedById = c.String(maxLength: 128),
                        FirstSegment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.Segments", t => t.FirstSegment_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.FirstSegment_ID);
            
            CreateTable(
                "dbo.StoryGenres",
                c => new
                    {
                        Story_ID = c.Int(nullable: false),
                        Genre_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Story_ID, t.Genre_ID })
                .ForeignKey("dbo.Stories", t => t.Story_ID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_ID, cascadeDelete: true)
                .Index(t => t.Story_ID)
                .Index(t => t.Genre_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories", "ModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoryGenres", "Genre_ID", "dbo.Genres");
            DropForeignKey("dbo.StoryGenres", "Story_ID", "dbo.Stories");
            DropForeignKey("dbo.Stories", "FirstSegment_ID", "dbo.Segments");
            DropForeignKey("dbo.Stories", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Genres", "ModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Genres", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Segments", "ModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ChildId", "dbo.Segments");
            DropForeignKey("dbo.Segments", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ParentId", "dbo.Segments");
            DropIndex("dbo.StoryGenres", new[] { "Genre_ID" });
            DropIndex("dbo.StoryGenres", new[] { "Story_ID" });
            DropIndex("dbo.Stories", new[] { "FirstSegment_ID" });
            DropIndex("dbo.Stories", new[] { "ModifiedById" });
            DropIndex("dbo.Stories", new[] { "CreatedById" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Genres", new[] { "ModifiedById" });
            DropIndex("dbo.Genres", new[] { "CreatedById" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Segments", new[] { "ModifiedById" });
            DropIndex("dbo.Segments", new[] { "CreatedById" });
            DropIndex("dbo.Actions", new[] { "ModifiedById" });
            DropIndex("dbo.Actions", new[] { "CreatedById" });
            DropIndex("dbo.Actions", new[] { "ChildId" });
            DropIndex("dbo.Actions", new[] { "ParentId" });
            DropTable("dbo.StoryGenres");
            DropTable("dbo.Stories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Genres");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Segments");
            DropTable("dbo.Actions");
        }
    }
}
