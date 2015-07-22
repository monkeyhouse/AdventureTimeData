using System.Collections.Generic;
using System.Linq;
using Data.Models;

namespace Business
{
    public class ModelFactory : IModelFactory
    {
        public ActionModel Create(Action action)
        {
            return new ActionModel()
                 {
                     ID = action.ID,
                     Text = action.Text                
                 };
        }

        //public ActionModel CreateActionWithSegments(Action action, Segment parent, Segment child);

        public GenreModel Create(Genre genre)
        {
            return new GenreModel()
                   {
                       ID = genre.ID,
                       Text = genre.Text
                   };
        }
        public SegmentModel Create(Segment segment)
        {
            return new SegmentModel()
                   {
                       ID = segment.ID,
                       Leaders = segment.Leaders.Select( Create),
                       Body = segment.Text,
                       Actions = segment.Actions.Select(Create),
                       IsEnding = segment.IsEnding
                   };
        }

        public StoryModel Create(Story story)
        {
            return new StoryModel()
                   {
                       ID = story.ID,
                       Title = story.Title,
                       Byline = story.Byline,
                       FirstSegment = Create(story.FirstSegment),
                       Generes = story.Genres.Select(Create)
                   };
        }
    }
}
