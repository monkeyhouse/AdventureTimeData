using System;
using Business.Models;

namespace Business.Repositories
{
    public class PageRepository : RepositoryBase, IPageRepository 
    {
        IActionRepository genreRepo;
        private IModelFactory mFactory;
        public PageRepository(IActionRepository genreRepository,  IModelFactory modelFactory)
        {
            genreRepo = genreRepository;
            mFactory = modelFactory;
        }

        public PageModel GetPage(int segmentId)
        {
            throw new System.NotImplementedException();
        }

        public PageModel GetRootPage(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public PageModel CreatePage(int actionId, string text)
        {
            throw new System.NotImplementedException();
        }

        public PageModel EndStory(int segmentId)
        {
            throw new System.NotImplementedException();
        }

        public PageModel UnendStory(int segmentId)
        {
            throw new NotImplementedException();
        }

        public bool DeletePage(int segementId)
        {
            throw new NotImplementedException();
        }
    }
}