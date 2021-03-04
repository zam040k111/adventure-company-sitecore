using Adventure.Feature.EventSearchComponent.Models;
using Sitecore.ContentSearch;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Repositories.Interfaces;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch.Linq;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;

namespace Adventure.Feature.EventSearchComponent.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private const string IndexName = "sitecore_web_index";

        private readonly ISitecoreService _sitecoreService;

        public EventsRepository()
        {
            _sitecoreService = new SitecoreService(Sitecore.Context.Database);
        }

        public IEnumerable<IEventDetailsPage> SearchEvents(
	        IEventPipeline pipeline,
	        EventSearchSettings searchSettings)
        {
            Throw.IfNull(searchSettings, nameof(searchSettings));
            Throw.IfNull(pipeline, nameof(pipeline));

            using (var ctx = ContentSearchManager.GetIndex(IndexName).CreateSearchContext())
            {
	            var queryable = ctx.GetQueryable<EventDetailsSearchItem>();
                var searchResults = pipeline.Process(searchSettings, queryable).GetResults();

                if (searchResults != null && searchResults.TotalSearchResults > 0)
                {
                    var data = searchResults.Hits
                        .Where(i => i.Document != null)
                        .Select(i => i.Document);

                    var listResult = new List<IEventDetailsPage>();

                    foreach (var searchItem in data)
                    {
                        listResult.Add(_sitecoreService.GetItem<IEventDetailsPage>(searchItem.GetItem()));
                    }

                    return listResult;
                }
            }

            return Enumerable.Empty<IEventDetailsPage>();
        }

        public IEnumerable<IEventTag> GetAllTags()
        {
			const string standardValues = "__Standard Values";
	        using (var ctx = ContentSearchManager.GetIndex(IndexName).CreateSearchContext())
	        {
		        var searchResults = ctx.GetQueryable<EventTagSearchItem>()
			        .Where(i => i.TemplateId == EventTag.TemplateId && i.Name != standardValues)
			        .GetResults();

		        if (searchResults != null && searchResults.TotalSearchResults > 0)
		        {
			        var data = searchResults.Hits
				        .Where(i => i.Document != null)
				        .Select(i => i.Document);

			        var listResult = new List<IEventTag>();

			        foreach (var searchItem in data)
			        {
				        listResult.Add(_sitecoreService.GetItem<IEventTag>(searchItem.GetItem()));
			        }

			        return listResult;
		        }
            }

	        return Enumerable.Empty<IEventTag>();
        }
    }
}
