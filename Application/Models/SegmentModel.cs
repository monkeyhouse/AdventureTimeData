using System.Collections.Generic;

namespace Business
{
    public class SegmentModel
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public bool IsEnding { get; set; }

        //nullables
        public IEnumerable<ActionModel> Leaders { get; set; }
        public IEnumerable<ActionModel> Actions { get; set; }
    }
}