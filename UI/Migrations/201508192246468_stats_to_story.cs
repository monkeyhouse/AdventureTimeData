namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stats_to_story : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StoryStats", "IX_Story_ID");
            DropPrimaryKey("dbo.StoryStats");
            AlterColumn("dbo.StoryStats", "StoryID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StoryStats", "StoryID");
            CreateIndex("dbo.StoryStats", "StoryID");
            DropColumn("dbo.StoryStats", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoryStats", "ID", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.StoryStats", new[] { "StoryID" });
            DropPrimaryKey("dbo.StoryStats");
            AlterColumn("dbo.StoryStats", "StoryID", c => c.Int());
            AddPrimaryKey("dbo.StoryStats", "ID");
            CreateIndex("dbo.StoryStats", "StoryID", name: "IX_Story_ID");
        }
    }
}
