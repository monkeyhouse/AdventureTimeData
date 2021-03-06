using System.Linq;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using UI.DbContext;
using UI.Models;
using UI.Queries.Models;
using System.Web.Http.Cors;

namespace UI.Controllers
{
    [BreezeController]
    [EnableCors("*", "*", "*")]
    public class TagsController : ApiController
    {
        readonly EFContextProvider<AdventureTimeDbContext> _contextProvider =
            new EFContextProvider<AdventureTimeDbContext>();

        // ~/breeze/tags/Metadata 
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        // ~/breeze/tags/Tags
        // ~/breeze/tags/Tags?$filter=IsArchived eq false&$orderby=CreatedAt 
        [HttpGet]
        public IQueryable<Tag> Tags()
        {
            var mFac = new ModelFactory();
            return _contextProvider.Context.Tags;
        }

        // ~/breeze/tags/SaveChanges
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }
    }
}