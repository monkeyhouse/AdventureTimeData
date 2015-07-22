using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;
using Data.Models;
using Microsoft.Practices.Unity;

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

        public StoryModel GetStory(int id)
        {
            return mFactory.Create(dbContext.Stories.Find(id));
        }

        public StoryModel CreateStory(StoryEditModel story)
        {            
            var genres = genreRepo.FindorCreateGenres(story.Generes);
            var segment = new Segment() { Text = story.FirstSegment.Body };
            var model = new Story() {Title = story.Title, Byline = story.Byline, FirstSegment = segment, Genres = genres};

            dbContext.Segments.Add(segment);
            dbContext.Stories.Add(model);
            dbContext.SaveChanges();

            return mFactory.Create(model);
        }

        public StoryModel UpdateStory(int id, StoryEditModel value)
        {
            var story = dbContext.Stories.Find(id);
            story.Title = value.Title;
            story.Genres = genreRepo.FindorCreateGenres(value.Generes);
            story.Byline = value.Byline;
            story.FirstSegment = UpdateFirstSegment(value.FirstSegment);

            dbContext.Entry(story).State = EntityState.Modified;
            dbContext.Entry(story.FirstSegment).State = EntityState.Modified;
            dbContext.SaveChanges();

            return mFactory.Create(story);
        }

        private Segment UpdateFirstSegment(SegmentModel model)
        {
            var s = dbContext.Segments.Find(model.ID);
            s.Text = model.Body;
            s.IsEnding = model.IsEnding;
            return s;
        }

        public bool DeleteStory(int id)
        {
            //if dbContext.Stories it not attached?
            var story = new Story {ID = id};
            dbContext.Stories.Attach(story);
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

        public void Complete()
        {
            dbContext.SaveChanges();            
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                Complete();
                dbContext.Dispose();
            }

            dbContext = null;
        }
    }

    public class RepositoryBase
    {
        protected AdventureTimeModel dbContext;

        [Dependency]
        public AdventureTimeModel DbContext {
            get { return dbContext; }
            set { dbContext = value; }
        }
    }
}
