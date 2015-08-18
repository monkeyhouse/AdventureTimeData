using UI.Models;

namespace UI.Queries.Models
{
    public interface IModelFactory
    {
        TagModel Create(Tag tag);
        PageModel Create(Page page);
        StoryModel Create(Story story);
    }
}