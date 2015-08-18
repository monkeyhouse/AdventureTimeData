using System.Web.Http;
using Breeze.WebApi2;
using Breeze.ContextProvider.EF6;
using UI.DbContext;
using UI.Models;
using System.Linq;
using System.Web.Http.Cors;

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
            return _contextProvider.Context.Stories;
        }
    }
}