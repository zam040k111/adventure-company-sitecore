using System.Collections.Generic;
using Adventure.Feature.EventSearchComponent.Filtering;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Infrastructure;
using Adventure.Feature.EventSearchComponent.Models;
using Adventure.Feature.EventSearchComponent.Repositories.Interfaces;
using Adventure.Feature.EventSearchComponent.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using log4net;

namespace Adventure.Feature.EventSearchComponent.Services
{
    public class EventSearchService : IEventSearchService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IEventPipeline _eventPipeline;
        private readonly ILog _logger;

        public EventSearchService(
            IEventsRepository eventsRepository,
            IEventPipeline eventPipeline,
            ILog logger)
        {
            Throw.IfNull(eventsRepository, nameof(eventsRepository));
            Throw.IfNull(eventPipeline, nameof(eventPipeline));

            _eventsRepository = eventsRepository;
            _eventPipeline = eventPipeline;
            _logger = logger;
        }

        public EventListViewModel Search(EventSearchSettings searchSettings)
        {
            Throw.IfNull(searchSettings, nameof(searchSettings));

            _logger.Debug(
                $"{nameof(Search)} has been called with name {searchSettings.Name}," +
                $" difficulty {searchSettings.Difficulty}, starDate {searchSettings.StartDate}," +
                $" orderBy {searchSettings.OrderBy}, page number {searchSettings.PageNumber}," +
                $" page size {searchSettings.PageSize}.");

            _eventPipeline
                .Register(new NameFilter())
                .Register(new DifficultyFilter())
                .Register(new DateFilter())
                .Register(new TagFilter())
                .Register(new SortByFilter());

            var filteredEvents = _eventsRepository.SearchEvents(_eventPipeline, searchSettings);
            var filteredEventsViewModel = filteredEvents.ToViewModel();

            return new EventListViewModel
            {
                FilteredItems = filteredEventsViewModel,
                TotalItems = _eventPipeline.TotalItems,
                PageNumber = searchSettings.PageNumber,
                PageSize = searchSettings.PageSize
            };
        }

        public IEnumerable<IEventTag> GetAllTags() => _eventsRepository.GetAllTags();
    }
}
