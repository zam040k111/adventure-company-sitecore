using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventDetailsProvider.Repositories.Interfaces;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using log4net;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Adventure.Feature.EventDetailsProvider.Repositories
{
    public class EventListRepository : IEventListRepository
    {
        private readonly ID _standardValueId = new ID("{5D814457-929D-4B07-B31C-B363A3F88B08}");
        private readonly ILog _logger;
        private readonly IEventProviderSettingsReader _eventProviderSettingsReader;

        public EventListRepository(
            ILog logger,
            IEventProviderSettingsReader eventProviderSettingsReader)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(eventProviderSettingsReader, nameof(eventProviderSettingsReader));

            _logger = logger;
            _eventProviderSettingsReader = eventProviderSettingsReader;
        }

        public IEnumerable<Item> GetAll()
        {
            _logger.Debug($"{nameof(GetAll)} has been called.");

            using (var ctx = ContentSearchManager.GetIndex(_eventProviderSettingsReader.SearchIndex)
                .CreateSearchContext())
            {
                var queryable = ctx.GetQueryable<SearchResultItem>();
                var searchResults = queryable
                    .Where(x => x.TemplateId == EventListPage.TemplateId
                                && x.ItemId != _standardValueId)
                    .GetResults();

                if (searchResults != null && searchResults.TotalSearchResults > 0)
                {
                    var data = searchResults.Hits
                        .Where(i => i.Document != null)
                        .Select(i => i.Document);

                    return data.Select(x => x.GetItem());
                }

                return Enumerable.Empty<Item>();
            }
        }
    }
}
