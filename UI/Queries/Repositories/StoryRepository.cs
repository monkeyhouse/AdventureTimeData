using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Business.Models;
using Data.Models;

namespace Business.Repositories
{
    public class StoryRepository : RepositoryBase, IStoryRepository, IDisposable, ICompletable
    {
        IGenreRepository genreRepo;
        private IModelFactory mFactory;
        public StoryRepository(IGenreRepository genreRepository,  IModelFactory modelFactory)
        {

            genreRepo = genreRepository;
            mFactory = modelFactory;
        }

        public IQueryable<StoryModel> GetStories(int page, string titleFilter, int[] genreFilters, int pageSize = 10)
        {
            var stories = dbContext.Stories;
            var result = stories.Select(t => t);

            if (titleFilter != null)
                result = result.Where(t => t.Title.StartsWith(titleFilter));

            if (genreFilters != null)
                result = result.Where(t => t.Genres.Any(g => genreFilters.Contains(g.ID)));

            return result.Skip(page*pageSize).Take(pageSize).Select( t => mFactory.Create(t));
        }

        public StoryModel GetStory(int storyId)
        {
            return mFactory.Create(dbContext.Stories.Find(storyId));
        }

        public StoryModel CreateStory(StoryEditModel story)
        {            
            var genres = genreRepo.FindorCreateGenres(story.Generes);
            var segment = new Page() { Text = story.FirstPage.Body };
            var model = new Story() {Title = story.Title, Summary = story.Summary, FirstPage = segment, Genres = genres};

            dbContext.Pages.Add(segment);
            dbContext.Stories.Add(model);
            dbContext.SaveChanges();

            return mFactory.Create(model);
        }

        public StoryModel UpdateStory(StoryEditModel storyModel)
        {
            var story = dbContext.Stories.Find(storyModel.ID);
            story.Title = storyModel.Title;
            story.Genres = genreRepo.FindorCreateGenres(storyModel.Generes);
            story.Summary = storyModel.Summary;
            story.FirstPage = UpdateFirstSegment(storyModel.FirstPage);

            dbContext.Entry(story).State = EntityState.Modified;
            dbContext.Entry(story.FirstPage).State = EntityState.Modified;
            dbContext.SaveChanges();

            return mFactory.Create(story);
        }

        private Page UpdateFirstSegment(PageModel model)
        {
            var s = dbContext.Pages.Find(model.ID);
            s.Text = model.Body;
            s.IsEnding = model.IsEnding;
            return s;
        }

        public bool DeleteStory(int storyId)
        {
            //if dbContext.Stories it not attached?
            var story = dbContext.Stories.Find(storyId);
            dbContext.Stories.Remove(story);
            dbContext.SaveChanges();
            return true;
        }

    

        public StoryStatsModel GetStoryStats(int id)
        {
            var stats = new StoryStatsModel();
            var story  = dbContext.Stories.Find(id);
            //todo: build stats
            //public int NodeCount { get; set; }
            //public int MaxDepth { get; set; }
            //public int CompletionPercent { get; set; }
            return stats;
        }

        public StoryTreeModel GetStoryTree(int id)
        {
            var treeModel = new StoryTreeModel();
            var story = dbContext.Stories.Find(id);
            //todo: build tree model
            //public string Title { get; set; }
            //public string Byline { get; set; }
            //public StoryTreeNode Root {get; set; }
            return treeModel;
        }

   
    }
}
