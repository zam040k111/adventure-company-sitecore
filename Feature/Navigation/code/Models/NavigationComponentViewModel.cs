using System;
using System.Collections.Generic;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Navigation.Models
{
    public class NavigationComponentViewModel
    {
        public string Title { get; set; }

        public Guid CurrentEventList { get; set; }

        public Guid CurrentEventPage { get; set; }

        public IEnumerable<IEventListPage> EventLists { get; set; }

        public bool IsEventDetailsPage { get; set; }
    }
}
