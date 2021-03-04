namespace Adventure.Feature.EventSearchComponent.Models
{
    public class EventViewModel
    {
        public string Title { get; set; }

        public string StartDate { get; set; }

        public int Difficulty { get; set; }

        public string Url { get; set; }

        public string[] Tags { get; set; }
    }
}
