using System;
using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Utilities
{
    public class EventSourceReader : IEventSourceReader
    {
        private static readonly Random Random = new Random();
        private readonly ILog _logger;
        private readonly IEventDataProvider _eventDataProvider;

        public EventSourceReader(
            ILog logger,
            IEventDataProvider eventDataProvider)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(eventDataProvider, nameof(eventDataProvider));

            _logger = logger;
            _eventDataProvider = eventDataProvider;
        }
        public string ReadSourceSectionValue(string sectionName)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(sectionName), nameof(sectionName),
                Sitecore.Globalization.Translate.Text(DictionaryKeys.IncorrectSectionName));

            _logger.Debug($"{nameof(ReadSourceSectionValue)} has been called with section {sectionName}.");

            string result;

            try
            {
                var collectionOfBeginnings = _eventDataProvider.Data[$"{sectionName}{EventSourceSections.Beginning}"];
                var collectionOfCenters = _eventDataProvider.Data[$"{sectionName}{EventSourceSections.Center}"];
                var collectionOfEndings = _eventDataProvider.Data[$"{sectionName}{EventSourceSections.Ending}"];

                result = $"{collectionOfBeginnings[Random.Next(0, collectionOfBeginnings.Count)]} " +
                         $"{collectionOfCenters[Random.Next(0, collectionOfCenters.Count)]} " +
                         $"{collectionOfEndings[Random.Next(0, collectionOfEndings.Count)]}";
            }
            catch(Exception)
            {
                throw new Exception("Section name is not correct.");
            }

            return result;
        }
    }
}
