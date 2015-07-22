using Data.Models;

namespace Business
{
    public class GenreModel
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", ID,Text);
        }
    }
}
