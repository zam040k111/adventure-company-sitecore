using System.Web.Mvc;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Controllers
{
    public class EventProviderController : BaseController
    {
        private readonly ILog _logger;
        private readonly IRandomEventService _randomEventService;
        private readonly ISerializingService _serializingService;

        public EventProviderController(
            ILog logger,
            IRandomEventService randomEventService,
            ISerializingService serializingService)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(randomEventService, nameof(randomEventService));
            Throw.IfNull(serializingService, nameof(serializingService));

            _logger = logger;
            _randomEventService = randomEventService;
            _serializingService = serializingService;
        }

        [HttpGet]
        public string GetEvent()
        {
            _logger.Debug($"{nameof(GetEvent)} has been called.");

            var randomEvent = _randomEventService.GenerateNewEvent();
            var responseString = _serializingService.SerializeEvent(randomEvent);

            return responseString;
        }
    }
}
