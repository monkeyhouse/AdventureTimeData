namespace UI.Queries.Models
{
    public class TagModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool IsApproved { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", ID,Text);
        }
    }
}
