using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.DbContext;
using UI.Models;
using UI.Models.Stats;
using System.Web.Http.Results;
using System.Web;

namespace UI.Controllers
{

    public class UserAuth
    {
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
        public UserAuth()
        {
            var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var me = manager.FindByName("sam"); //.FindById(user)
            var userIdLog = me;
            UserID = me.Id;
            User = me;
        }
    }

    /// <summary>
    /// Contorller used to manage UserStoryStats data
    /// </summary>
    [Authorize]
    public class StoryStatsController : ApiController
    {
        private readonly AdventureTimeDbContext _dbContext;

        private readonly string _userID;
        private readonly IdentityUser _user;

        private readonly int _storyID;
        private readonly int _pageID;
        private readonly UserStoryStats _stats;
        private readonly StoryStats _storyStats;
        private readonly Story _story;
        private readonly Page _page;


        //Url must be StoryStats/:StoryID/Action/values
        public StoryStatsController()
        {
            var auth = new UserAuth(); //DI this

            _userID = auth.UserID;
            _user = auth.User;

            _dbContext = new AdventureTimeDbContext( );
            _dbContext.userID = auth.User;

            //validate and set route params
            var r = HttpContext.Current.Request.RequestContext.RouteData;
            var storyID = r.Values["storyID"];
            if (storyID == null||
                !int.TryParse((string)storyID, out _storyID) ||
                (_story = _dbContext.Stories.Find(_storyID)) == null){
                throw new HttpRequestException("Invalid Route. Route must contain storyID");
            }

            var pageID = r.Values["pageID"];
            //if page in route then validate page
            if (pageID != null &&
                ((!int.TryParse((string)pageID, out _pageID) ||
                 (_page = _dbContext.Pages.Find(_pageID)) == null) ||
                 (_page.Story.ID != _storyID)))
            {
                throw new HttpRequestException("Invalid Route. pageID is Invalid");
                //pageID is invalid or does not match story
            }

            //to enforce: story stats must be created when story is created 1:1
            _storyStats = _dbContext.StoryStats.Single(t => t.Story.ID == _storyID);
            _stats = _dbContext.UserStoryStats.SingleOrDefault(t => t.UserID == _userID && t.Story.ID == _storyID);
            if (_stats == default(UserStoryStats))
            {
                _stats = new UserStoryStats { UserID = _userID, Story = _story };
                _dbContext.UserStoryStats.Add(_stats);
                _dbContext.SaveChanges();
            }
        }



        [HttpPost]
        public IHttpActionResult ToggleLike(string val)
        {
            var value = val == "t";
            if (_stats.IsLike != value)
            {
                _stats.IsLike = value;
                _storyStats.Likes += (value ? 1 : -1);
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ToggleFavorite(bool value)
        {
            if (_stats.IsFavorite != value)
            {
                _stats.IsFavorite = value;
                _storyStats.Favorites += (value ? 1 : -1);
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult PlayStory()
        {
            if (_stats.Plays == 0){
                //user views can only count one view per user
                _storyStats.Views++;
            }
            _stats.Plays++;            
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult TakeAction()
        {
            _stats.Actions++;
            return Ok();
        }


        [HttpPost]
        public IHttpActionResult ItsAMinute()
        {
            _stats.Minutes++;
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult PageVisited()
        {
            _stats.Pages++;
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Complete(int PageID)
        {
            _stats.Completions++;
            _storyStats.Completions++;

            var completion = new UserStoryEnding()
            {
                Story = _story,
                UserID = _userID,
                Page = _page
            };
            _dbContext.UserStoryEndings.Add(completion);
            return Ok();
        }

        protected override OkResult Ok()
        {
            _dbContext.SaveChanges();
            return base.Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (_dbContext != null){
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
        
    }
}
