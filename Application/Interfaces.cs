using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Business
{
    public interface ISegmentRepo
    {
        SegmentModel GetSegment(int segmentId);
        SegmentModel GetRootSegment(int storyId);
        SegmentModel CreateSegment(int preceedingSegmentId, string action, string text);
        SegmentModel EndStory(int segmentId);
    }

    public interface IStoryRepo
    {
        StoryModel CreateStory(string title, string byline, List<Genre> genres);
        StoryModel LockStory(int storyId);
        StoryModel GetStory(int storyId);
    }

    public interface IStoryTreeRepo
    {
        object GetStoryTree(int storyId);
        object GetTreeStatistics(int storyId);        
    }

    public interface IActionRepo
    {
        
        ActionModel CreateBackAction(int segmentIdFrom, int segmentIdTo, string text);
    }

    public interface IStoryRepository
    {
        IQueryable<StoryModel> GetStories(int page, string titleFilter, int[] genreFilters, int pageSize = 10);
        StoryModel GetStory(int id);
        StoryModel CreateStory(StoryEditModel story);
        StoryModel UpdateStory(int id, StoryEditModel value);
        bool DeleteStory(int id);

        StoryStatsModel GetStoryStats(int id);
        StoryTreeModel GetStoryTree(int id);
    }

    public interface IGenreRepository
    {
        //internal use
        //Genre Create(GenreModel g);
        ICollection<Genre> FindorCreateGenres(IEnumerable<GenreModel> genres);
    }

    public interface ICompletable
    {
        void Complete();
    }

    public interface ISegmentRepository
    {
        SegmentModel CreateSegment(int storyId, int parentSegmentId, SegmentModel model);
    }

}
