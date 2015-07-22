using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Business;
using Business.Repositories;

namespace UI
{
  
    public class StoriesController : ApiController
    {
        private IStoryRepository repo;
        public StoriesController()
        {
            //repo = new StoryRepository(HttpContext.Current.User.Identity.Name);
        }

        ///<summary>
        ///Returns a page of stories filtered by title & genre
        ///</summary>
        [Route("")]
        // GET api/stories?page=x
        public IEnumerable<StoryModel> Get(int page = 0, string titleFilter = null, int[] genreFilters = null )
        {
            return repo.GetStories(page, titleFilter, genreFilters);
        }

        // GET api/stories/5
        public IHttpActionResult Get(int id)
        {
            var story = repo.GetStory(id);

            if (story == null)
                return NotFound();

            return Ok(story);
        }


        // POST api/stories -- create story
        public IHttpActionResult Post([FromBody]StoryEditModel value)
        {
            StoryModel story;
            try{ story = repo.CreateStory(value); }
            catch(Exception ex){ return InternalServerError(ex); }

            var uri = Url.Route("DefaultApi", new {httpRoute = "", controller = "stories", id = story.ID});
            return Created(uri, story);
        }

        // PUT api/stories/5
        public IHttpActionResult Put(int id, [FromBody]StoryEditModel value)
        {
            StoryModel story;
            try { story = repo.UpdateStory(id, value); }
            catch (Exception ex) { return InternalServerError(ex); }
            return Ok(story);
        }

        // DELETE api/stories/5
        public IHttpActionResult Delete(int id)
        {
            //delete story
            try{ repo.DeleteStory(id); }
            catch (Exception ex) { return InternalServerError(ex); }
            return Ok();
        }


        // GET api/stories/5/stats
        [Route("~/api/stories/{id:int}/stats")]
        public object GetStat(int id)
        {
            return repo.GetStoryStats(id);
        }

        // GET api/stories/5/tree
        [Route("~/api/stories/{id:int}/tree/")]
        public object GetTree(int id)
        {
            return repo.GetStoryTree(id);
        }
    }
}