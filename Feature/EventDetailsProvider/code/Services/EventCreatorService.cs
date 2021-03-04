using System;
using System.Threading.Tasks;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class EventCreatorService : IEventCreatorService
    {
        private readonly IProviderCallerService _providerCallerService;
        private readonly IResponseParserService _responseParserService;
        private readonly IDatabaseWorkerService _databaseWorkerService;
        private readonly IEventProviderSettingsReader _eventProviderSettingsReader;
        private readonly ILog _logger;

        public EventCreatorService(
            IProviderCallerService providerCallerService,
            IResponseParserService responseParserService,
            IDatabaseWorkerService databaseWorkerService,
            IEventProviderSettingsReader eventProviderSettingsReader,
            ILog logger)
        {
            Throw.IfNull(providerCallerService, nameof(providerCallerService));
            Throw.IfNull(responseParserService, nameof(responseParserService));
            Throw.IfNull(databaseWorkerService, nameof(databaseWorkerService));
            Throw.IfNull(eventProviderSettingsReader, nameof(eventProviderSettingsReader));
            Throw.IfNull(logger, nameof(logger));

            _providerCallerService = providerCallerService;
            _responseParserService = responseParserService;
            _databaseWorkerService = databaseWorkerService;
            _eventProviderSettingsReader = eventProviderSettingsReader;
            _logger = logger;
        }

        public async Task CreateEvent()
        {
            _logger.Debug($"{nameof(CreateEvent)} has been called at {DateTime.Now}");

            var responseString = await _providerCallerService.GetData(_eventProviderSettingsReader.RequestUrl);

            var newEvent = _responseParserService.GetNewEventFromString(responseString);

            _databaseWorkerService.AddEvent(newEvent);
        }
    }
}
