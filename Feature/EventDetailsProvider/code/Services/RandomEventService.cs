using System;
using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class RandomEventService : IRandomEventService
    {
        private readonly ILog _logger;
        private readonly IEventSourceReader _eventSourceReader;
        private static readonly Random Random = new Random();

        public RandomEventService(
            ILog logger,
            IEventSourceReader eventSourceReader)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(eventSourceReader, nameof(eventSourceReader));

            _logger = logger;
            _eventSourceReader = eventSourceReader;
        }

        public NewEvent GenerateNewEvent()
        {
            _logger.Debug($"{nameof(GenerateNewEvent)} has been called.");

            var newEvent = new NewEvent();

            newEvent.Description = _eventSourceReader.ReadSourceSectionValue(EventDetailsPage.Fields.Description_FieldName);
            newEvent.Difficulty = Random.Next(1, 10);
            newEvent.ButtonText = _eventSourceReader.ReadSourceSectionValue(EventDetailsPage.Fields.ButtonText_FieldName);
            newEvent.ShortDescription = _eventSourceReader.ReadSourceSectionValue(EventDetailsPage.Fields.ShortDescription_FieldName);
            newEvent.StartDate = DateTime.Now.AddDays(Random.Next(3, 30));
            newEvent.EndDate = DateTime.Now.AddDays(Random.Next(30, 60));
            newEvent.Title = _eventSourceReader.ReadSourceSectionValue(EventDetailsPage.Fields.Title_FieldName);

            return newEvent;
        }
    }
}
