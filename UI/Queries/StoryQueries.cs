using System;
using System.Linq;

using UI.DbContext;
using UI.Queries.Models;

namespace UI.Queries
{
    public class QueryBase :  IDisposable
    {
         protected AdventureTimeDbContext dbContext;

        //[Dependency]
        public AdventureTimeDbContext DbContext {
            get { return dbContext; }
            set { dbContext = value; }
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();

            dbContext = null;
        }

        
    }

    public class StoryQueries : QueryBase
    {
        private readonly IModelFactory _modelFactory;

        public StoryQueries(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public IQueryable<StoryModel> GetStories(int page, int pageSize, string titleFilter, int[] genreFilters)
        {
            var stories = dbContext.Stories;
            var result = stories.Select(t => t);

            if (titleFilter != null)
                result = result.Where(t => t.Title.StartsWith(titleFilter));

            if (genreFilters != null)
                result = result.Where(t => t.Tags.Any(g => genreFilters.Contains(g.ID)));

            return result.Skip(page * pageSize).Take(pageSize).Select(t => _modelFactory.Create(t));
        }


        //        .Statistics(out stats)
        //        .Paging(currentPage, defaultPage, pageSize)
        //        .OrderByDescending(post => post.PublishAt)

        // i really like the LINQ extension methods that JimmyBogard uses
        // pattern from lostechies.com

        //public List<StoryModel> Execute(int currentPage, int defaultPage, int pageSize, out RavenQueryStatistics stats)

        //{
        //    return dbContext.Query<Post>()
        //        .Include(x => x.AuthorId)
        //        .Statistics(out stats)
        //        .WhereIsPublicPost()
        //        .OrderByDescending(post => post.PublishAt)
        //        .Paging(currentPage, defaultPage, pageSize)
        //        .ToList();
        //}
    }
}