using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.Navigation.Models;
using Adventure.Feature.Navigation.Repositories.Interfaces;
using Adventure.Feature.Navigation.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;

namespace Adventure.Feature.Navigation.Repositories
{
    public class EventListRepository : IEventListRepository
    {
        private readonly ID _standardValuesId = new ID("{5D814457-929D-4B07-B31C-B363A3F88B08}");
        private readonly ID _templateId = new ID("{FB686A69-7EA4-462F-91ED-4610EF0BA908}");
        private readonly ISitecoreService _sitecoreService;
        private readonly ISearchSettingsReader _searchSettingsReader;

        public EventListRepository(
            ISitecoreService sitecoreService,
            ISearchSettingsReader searchSettingsReader)
        {
            Throw.IfNull(sitecoreService, nameof(sitecoreService));
            Throw.IfNull(searchSettingsReader, nameof(searchSettingsReader));

            _searchSettingsReader = searchSettingsReader;
            _sitecoreService = sitecoreService;
        }

        public IEnumerable<IEventListPage> GetEventLists()
        {
            var settings = _searchSettingsReader.Settings;

            Throw.IfNull(settings, nameof(settings));

            using (var ctx = ContentSearchManager.GetIndex(settings.IndexName).CreateSearchContext())
            {
                var queryable = ctx.GetQueryable<EventListSearchResultItem>();
                var searchResults = queryable
                    .Where(x=>x.TemplateId == _templateId 
                              && x.ItemId != _standardValuesId
                              && !x.ExcludeFromNavigation)
                    .OrderBy(x=>x.Name)
                    .GetResults();

                if (searchResults != null && searchResults.TotalSearchResults > 0)
                {
                    var data = searchResults.Hits
                        .Where(i => i.Document != null)
                        .Select(i => i.Document);

                    var items = data.Select(x => x.GetItem());
                    var eventLists = items.Select(x => _sitecoreService.GetItem<IEventListPage>(x));

                    return eventLists;
                }
            }

            return Enumerable.Empty<IEventListPage>();
        }
    }
}
