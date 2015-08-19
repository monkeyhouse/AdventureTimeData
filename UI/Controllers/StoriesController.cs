using System.Web.Http;
using Breeze.WebApi2;
using Breeze.ContextProvider.EF6;
using UI.DbContext;
using UI.Models;
using System.Linq;
using System.Web.Http.Cors;
using UI.Models.Stats;

namespace UI.Controllers
{

    [BreezeController]
    [EnableCors("*","*","*")]
    public class StoriesController : ApiController
    {

        readonly EFContextProvider<AdventureTimeDbContext> _contextProvider =
        new EFContextProvider<AdventureTimeDbContext>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        // ~/breeze/Stories/Stories?$filter=IsArchived eq false&$orderby=CreatedAt 
        [HttpGet]
        public IQueryable<Story> Stories()
        {
            return _contextProvider.Context.Stories.Include("Tags").Include("StoryStats");
        }

        [HttpGet]
        public IQueryable<StoryStats> StoryStats()
        {
            return _contextProvider.Context.StoryStats;
        }

        [HttpGet]
        public IQueryable<object> Tags()
        {
            return _contextProvider.Context.Stories.Include("Tags");
        }

    }
}