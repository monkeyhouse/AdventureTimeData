namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stat_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoryStats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Likes = c.Int(nullable: false),
                        Favorites = c.Int(nullable: false),
                        Plays = c.Int(nullable: false),
                        Completions = c.Int(nullable: false),
                        Nodes = c.Int(nullable: false),
                        Actions = c.Int(nullable: false),
                        Endings = c.Int(nullable: false),
                        StoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .Index(t => t.StoryID, name: "IX_Story_ID");
            
            CreateTable(
                "dbo.UserStoryEndings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        PageID = c.Int(),
                        StoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pages", t => t.PageID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PageID, name: "IX_Page_ID")
                .Index(t => t.StoryID, name: "IX_Story_ID");
            
            CreateTable(
                "dbo.UserStoryStats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        Plays = c.Int(nullable: false),
                        Pages = c.Int(nullable: false),
                        Actions = c.Int(nullable: false),
                        Minutes = c.Int(nullable: false),
                        Completions = c.Int(nullable: false),
                        IsFavorite = c.Boolean(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                        StoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stories", t => t.StoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.StoryID, name: "IX_Story_ID");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStoryStats", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserStoryStats", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.UserStoryEndings", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserStoryEndings", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.UserStoryEndings", "PageID", "dbo.Pages");
            DropForeignKey("dbo.StoryStats", "StoryID", "dbo.Stories");
            DropIndex("dbo.UserStoryStats", "IX_Story_ID");
            DropIndex("dbo.UserStoryStats", new[] { "UserID" });
            DropIndex("dbo.UserStoryEndings", "IX_Story_ID");
            DropIndex("dbo.UserStoryEndings", "IX_Page_ID");
            DropIndex("dbo.UserStoryEndings", new[] { "UserID" });
            DropIndex("dbo.StoryStats", "IX_Story_ID");
            DropTable("dbo.UserStoryStats");
            DropTable("dbo.UserStoryEndings");
            DropTable("dbo.StoryStats");
        }
    }
}
