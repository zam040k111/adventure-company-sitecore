using System.Net;
using System.Threading.Tasks;
using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class ProviderCallerService : IProviderCallerService
    {
        private readonly IResponseDataStreamReaderService _dataStreamReaderService;
        private readonly ILog _logger;

        public ProviderCallerService(
            IResponseDataStreamReaderService dataStreamReaderService,
            ILog logger)
        {
            Throw.IfNull(dataStreamReaderService, nameof(dataStreamReaderService));
            Throw.IfNull(logger, nameof(logger));

            _dataStreamReaderService = dataStreamReaderService;
            _logger = logger;
        }

        public async Task<string> GetData(string requestUrl)
        {
            Throw.IfCondition(string.IsNullOrEmpty(requestUrl), nameof(requestUrl),
                Sitecore.Globalization.Translate.Text(DictionaryKeys.IncorrectUrl));

            _logger.Debug($"{nameof(GetData)} has been called.");

            var request = WebRequest.Create(requestUrl);
            request.Credentials = CredentialCache.DefaultCredentials;

            var response = await request.GetResponseAsync();

            var responseContent = _dataStreamReaderService.ReadStream(response);

            return responseContent;
        }
    }
}
