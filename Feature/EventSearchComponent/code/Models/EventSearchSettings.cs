using System;

namespace Adventure.Feature.EventSearchComponent.Models
{
    public class EventSearchSettings
    {
	    public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public int? Difficulty { get; set; }

        public string OrderBy { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string[] Tags { get; set; }
    }
}
