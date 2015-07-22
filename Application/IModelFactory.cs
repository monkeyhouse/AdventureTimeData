using Data.Models;

namespace Business
{
    public interface IModelFactory
    {
        GenreModel Create(Genre genre);
        SegmentModel Create(Segment segment);
        StoryModel Create(Story story);
    }
}