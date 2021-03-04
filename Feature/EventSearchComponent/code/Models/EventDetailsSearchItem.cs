using System;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;

namespace Adventure.Feature.EventSearchComponent.Models
{
    public class EventDetailsSearchItem : SearchResultItem
    {
        [IndexField("title")]
        public string Title { get; set; }

        [IndexField("startdate")]
        public DateTime StartDate { get; set; }
        
        [IndexField("difficulty")]
        public int Difficulty { get; set; }

        [IndexField("tags")]
        public string[] TagsId { get; set; }
    }
}
