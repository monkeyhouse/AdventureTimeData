namespace Business
{
    public class StoryTreeModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Byline { get; set; }
        public StoryTreeNode Root {get; set; }
    }
}