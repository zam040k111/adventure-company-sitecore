using System;
using System.Collections.Generic;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.EventSearchComponent.Models
{
    public class EventListViewModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);

        public IEnumerable<EventViewModel> FilteredItems { get; set; }

        public IEnumerable<int> PageSizeOptions { get; set; }

        public IEnumerable<IEventTag> AllTags { get; set; }
    }
}
