using Business.Models;
using Data.Models;

namespace Business
{
    public interface IModelFactory
    {
        GenreModel Create(Genre genre);
        PageModel Create(Page page);
        StoryModel Create(Story story);
    }
}