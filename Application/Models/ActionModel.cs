namespace Business
{
    public class ActionModel
    {
        public int ID { get; set; }
        public string Text { get; set; }

        //null props except in special uses 
        public SegmentModel Parent { get; set; }
        public SegmentModel Child { get; set; }
    }
}