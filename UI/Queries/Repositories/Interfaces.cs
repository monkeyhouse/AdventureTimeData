using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Data.Models;

namespace Business
{

    public interface IStoryRepository
    {
        IQueryable<StoryModel> GetStories(int page, string titleFilter, int[] genreFilters, int pageSize = 10);
        StoryModel GetStory(int storyId);
        StoryModel CreateStory(StoryEditModel storyModel);
        StoryModel UpdateStory(StoryEditModel storyModel);
        bool DeleteStory(int storyId);
        //StoryModel LockStory(int storyId);

        StoryTreeModel GetStoryTree(int id);
    }

    public interface IGenreRepository
    {
        ICollection<Genre> FindorCreateGenres(IEnumerable<GenreModel> genres);
    }

    public interface IPageRepository
    {
        PageModel GetPage(int segmentId);
        PageModel GetRootPage(int storyId);
        PageModel CreatePage(int actionId, string text);

        PageModel EndStory(int segmentId);
        PageModel UnendStory(int segmentId);

        bool DeletePage(int segementId);
    }

    public interface IActionRepository
    {
        ActionModel GetAction(int actionId);
        ActionModel CreateAction(int parentSegmentId, string text);
        ActionModel UpdateAction(int parentSegmentId, string text, int? childSegmentId);
        ActionModel SetChild(int storyId, int actionid, int childSegmentId);
    }


    //public interface IStatsRepository
    //{
    //    StoryStatsModel GetStoryStats(int id);
    //    GenreStatsModel GetGenreStats(int id);
    //}


    public interface ICompletable
    {
        void Complete();
    }


    /* 
     *     public interface IPlotPointRepository
    {
        PageModel CreatePage(int storyId, int parentSegmentId, PageModel model);

        PlotPointModel CreatePlotPoint( PlotPointAddModel model );
        PlotPointModel UpdatePlotPoint( PlotPointModel model);
        //ActionModel CreateBackAction(int pageIdFrom, int segmentIdTo, string text);
    }


     */
}
