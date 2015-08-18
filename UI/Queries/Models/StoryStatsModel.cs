namespace UI.Queries.Models
{
    public class StoryStatsModel
    {
        public int ID { get; set; }
        public int NodeCount { get; set; }
        public int MaxDepth { get; set; }
        public int CompletionPercent { get; set; }
        //public int Views { get; set; }
    }

    public class GenreStatsModel
    {
        public int ID { get; set; }
        public int UseCount { get; set; }
        //average depth or depth distribution?
    }
}