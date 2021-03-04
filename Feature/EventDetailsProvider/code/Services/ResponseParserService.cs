using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;
using Newtonsoft.Json.Linq;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class ResponseParserService : IResponseParserService
    {
        private readonly ILog _logger;

        public ResponseParserService(ILog logger)
        {
            _logger = logger;
        }

        public NewEvent GetNewEventFromString(string responseContent)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(responseContent), nameof(responseContent),
                Sitecore.Globalization.Translate.Text(DictionaryKeys.IncorrectResponse));

            _logger.Debug($"{nameof(GetNewEventFromString)} has been called with response {responseContent}.");

            var responseObject = JObject.Parse(responseContent);
            var eventObject = responseObject.ToObject<NewEvent>();

            return eventObject;
        }
    }
}
