using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Data.Models;

namespace Business
{
    public class ModelFactory : IModelFactory
    {
        public ActionModel Create(Action action)
        {
            return new ActionModel()
                 {
                     ID = action.ID,
                     Text = action.Text                
                 };
        }

        public GenreModel Create(Genre genre)
        {
            return new GenreModel()
                   {
                       ID = genre.ID,
                       Text = genre.Text
                   };
        }
        public PageModel Create(Page page)
        {
            return new PageModel()
                   {
                       ID = page.ID,
                       Body = page.Text,
                       IsEnding = page.IsEnding
                   };
        }

        public PlotPointModel CreatePlotPoint(Page page)
        {
            return new PlotPointModel()
                   {
                       Page = Create(page),
                       Leaders = page.Leaders.Select(Create),
                       Actions = page.Actions.Select(Create)
                   };                   
        }

        public StoryModel Create(Story story)
        {
            return new StoryModel()
                   {
                       ID = story.ID,
                       Title = story.Title,
                       Summary = story.Summary,
                       FirstPage = Create(story.FirstPage),
                       Generes = story.Genres.Select(Create)
                   };
        }
    }
}
