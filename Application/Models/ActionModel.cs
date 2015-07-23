using Business.Models;

namespace Business
{

    public class ActionModel
    {
        public int ID { get; set; }
        public string Text { get; set; }

        //null props except in special uses 
        public PageModel Parent { get; set; }
        public PageModel Child { get; set; }
    }
}