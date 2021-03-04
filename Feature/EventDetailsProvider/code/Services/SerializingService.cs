using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;
using Newtonsoft.Json.Linq;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class SerializingService : ISerializingService
    {
        private readonly ILog _logger;

        public SerializingService(ILog logger)
        {
            Throw.IfNull(logger, nameof(logger));

            _logger = logger;
        }

        public string SerializeEvent(NewEvent newEvent)
        {
            Throw.IfNull(newEvent, nameof(newEvent));

            _logger.Debug($"{nameof(SerializingService)} has been called with event with title {newEvent.Title}.");

            var responseObject =  JObject.FromObject(newEvent);
            var responseString = responseObject.ToString(Newtonsoft.Json.Formatting.None);

            return responseString;
        }
    }
}
