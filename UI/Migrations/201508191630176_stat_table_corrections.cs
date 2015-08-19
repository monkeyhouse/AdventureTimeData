namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stat_table_corrections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoryStats", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.StoryStats", "Pages", c => c.Int(nullable: false));
            DropColumn("dbo.StoryStats", "Nodes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoryStats", "Nodes", c => c.Int(nullable: false));
            DropColumn("dbo.StoryStats", "Pages");
            DropColumn("dbo.StoryStats", "Views");
        }
    }
}
