using System.Collections.Generic;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.EventSearchComponent.Repositories.Interfaces
{
    public interface IEventsRepository
    {
        IEnumerable<IEventDetailsPage> SearchEvents(
            IEventPipeline pipeline,
	        EventSearchSettings searchSettings);

        IEnumerable<IEventTag> GetAllTags();
    }
}
