namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSegmentToPage : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Segments", newName: "Pages");
            DropForeignKey("dbo.Stories", "FirstSegment_ID", "dbo.Segments");
            DropIndex("dbo.Actions", new[] { "ParentId" });
            DropIndex("dbo.Actions", new[] { "ChildId" });
            DropIndex("dbo.Actions", new[] { "CreatedById" });
            DropIndex("dbo.Actions", new[] { "ModifiedById" });
            DropIndex("dbo.Pages", new[] { "CreatedById" });
            DropIndex("dbo.Pages", new[] { "ModifiedById" });
            DropIndex("dbo.Genres", new[] { "CreatedById" });
            DropIndex("dbo.Genres", new[] { "ModifiedById" });
            DropIndex("dbo.Stories", new[] { "CreatedById" });
            DropIndex("dbo.Stories", new[] { "ModifiedById" });
            DropIndex("dbo.Stories", new[] { "FirstSegment_ID" });
            RenameColumn(table: "dbo.Stories", name: "FirstSegment_ID", newName: "FirstPageID");
            AddColumn("dbo.Actions", "StoryID", c => c.Int(nullable: false));
            AddColumn("dbo.Pages", "PageNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Pages", "StoryID", c => c.Int(nullable: false));
            AddColumn("dbo.Genres", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stories", "Summary", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Stories", "Settings", c => c.String(maxLength: 250));
            AlterColumn("dbo.Stories", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Stories", "FirstPageID", c => c.Int(nullable: false));
            CreateIndex("dbo.Actions", "ParentID");
            CreateIndex("dbo.Actions", "ChildID");
            CreateIndex("dbo.Actions", "StoryID");
            CreateIndex("dbo.Actions", "CreatedByID");
            CreateIndex("dbo.Actions", "ModifiedByID");
            CreateIndex("dbo.Pages", "PageNumber", unique: true);
            CreateIndex("dbo.Pages", "StoryID");
            CreateIndex("dbo.Pages", "CreatedByID");
            CreateIndex("dbo.Pages", "ModifiedByID");
            CreateIndex("dbo.Stories", "FirstPageID");
            CreateIndex("dbo.Stories", "CreatedByID");
            CreateIndex("dbo.Stories", "ModifiedByID");
            CreateIndex("dbo.Genres", "CreatedByID");
            CreateIndex("dbo.Genres", "ModifiedByID");
            AddForeignKey("dbo.Actions", "StoryID", "dbo.Stories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Pages", "StoryID", "dbo.Stories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stories", "FirstPageID", "dbo.Pages", "ID", cascadeDelete: true);
            DropColumn("dbo.Stories", "Byline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stories", "Byline", c => c.String());
            DropForeignKey("dbo.Stories", "FirstPageID", "dbo.Pages");
            DropForeignKey("dbo.Pages", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.Actions", "StoryID", "dbo.Stories");
            DropIndex("dbo.Genres", new[] { "ModifiedByID" });
            DropIndex("dbo.Genres", new[] { "CreatedByID" });
            DropIndex("dbo.Stories", new[] { "ModifiedByID" });
            DropIndex("dbo.Stories", new[] { "CreatedByID" });
            DropIndex("dbo.Stories", new[] { "FirstPageID" });
            DropIndex("dbo.Pages", new[] { "ModifiedByID" });
            DropIndex("dbo.Pages", new[] { "CreatedByID" });
            DropIndex("dbo.Pages", new[] { "StoryID" });
            DropIndex("dbo.Pages", new[] { "PageNumber" });
            DropIndex("dbo.Actions", new[] { "ModifiedByID" });
            DropIndex("dbo.Actions", new[] { "CreatedByID" });
            DropIndex("dbo.Actions", new[] { "StoryID" });
            DropIndex("dbo.Actions", new[] { "ChildID" });
            DropIndex("dbo.Actions", new[] { "ParentID" });
            AlterColumn("dbo.Stories", "FirstPageID", c => c.Int());
            AlterColumn("dbo.Stories", "Title", c => c.String());
            DropColumn("dbo.Stories", "Settings");
            DropColumn("dbo.Stories", "Summary");
            DropColumn("dbo.Genres", "IsApproved");
            DropColumn("dbo.Pages", "StoryID");
            DropColumn("dbo.Pages", "PageNumber");
            DropColumn("dbo.Actions", "StoryID");
            RenameColumn(table: "dbo.Stories", name: "FirstPageID", newName: "FirstSegment_ID");
            CreateIndex("dbo.Stories", "FirstSegment_ID");
            CreateIndex("dbo.Stories", "ModifiedById");
            CreateIndex("dbo.Stories", "CreatedById");
            CreateIndex("dbo.Genres", "ModifiedById");
            CreateIndex("dbo.Genres", "CreatedById");
            CreateIndex("dbo.Pages", "ModifiedById");
            CreateIndex("dbo.Pages", "CreatedById");
            CreateIndex("dbo.Actions", "ModifiedById");
            CreateIndex("dbo.Actions", "CreatedById");
            CreateIndex("dbo.Actions", "ChildId");
            CreateIndex("dbo.Actions", "ParentId");
            AddForeignKey("dbo.Stories", "FirstSegment_ID", "dbo.Segments", "ID");
            RenameTable(name: "dbo.Pages", newName: "Segments");
        }
    }
}
