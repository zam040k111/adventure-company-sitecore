using System.Collections.Generic;
using Adventure.Feature.EventSearchComponent.Models;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.EventSearchComponent.Services.Interfaces
{
    public interface IEventSearchService
    {
        EventListViewModel Search(EventSearchSettings searchSettings);

        IEnumerable<IEventTag> GetAllTags();
    }
}
