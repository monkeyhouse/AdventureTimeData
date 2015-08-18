using System.Collections.Generic;

namespace UI.Queries.Models
{
    public class PageModel
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public bool IsEnding { get; set; }

    }

    public class PlotPointAddModel
    {
        public PageModel Page;
        public ActionModel Leader { get; set; }
    }

    public class PlotPointModel
    {
        public PageModel Page;
        public IEnumerable<ActionModel> Leaders { get; set; }
        public IEnumerable<ActionModel> Actions { get; set; }
    }
}