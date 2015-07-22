using System;
using System.Collections.Generic;
using System.Linq;
using Business;

namespace Business
{
    public class StoryModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Byline { get; set; }
        public IEnumerable<GenreModel> Generes { get; set; }
        public SegmentModel FirstSegment { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0} Title: {1} \n Byline:{2} \n Genres:[{3}] FirstSegment.Body:{4}", ID, Title, Byline, string.Join(",", Generes.Select(t=> t.Text)), FirstSegment.Body);
        }
    }

    public class StoryEditModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Byline { get; set; }
        public IEnumerable<GenreModel> Generes { get; set; }
        public SegmentModel FirstSegment { get; set; }
    }
}