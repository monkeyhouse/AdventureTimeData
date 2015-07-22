using System.Collections.Generic;

namespace Business
{
    public class StoryTreeNode
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public int ActionID { get; set; }
        public string ActionText { get; set; }

        public List<StoryTreeNode> Children { get; set; }
    }
}
