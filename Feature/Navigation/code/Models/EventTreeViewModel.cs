using System;
using System.Collections.Generic;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Navigation.Models
{
    public class EventTreeViewModel
    {
        public Guid CurrentEventPageId { get; set; }

        public IEnumerable<IEventDetailsPage> EventPages { get; set; }
    }
}
