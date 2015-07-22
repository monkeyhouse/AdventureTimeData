using System.Collections.Generic;

namespace Data.Models
{
    public class Story : Metadata
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Byline { get; set; }

        public bool IsLocked { get; set; }
        
        //Navigation Properties
        public virtual Segment FirstSegment { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        
    }
}