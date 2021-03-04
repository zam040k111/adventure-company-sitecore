using System.IO;
using System.Net;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class ResponseDataStreamReaderService : IResponseDataStreamReaderService
    {
        private readonly ILog _logger;

        public ResponseDataStreamReaderService(ILog logger)
        {
            Throw.IfNull(logger, nameof(logger));

            _logger = logger;
        }

        public string ReadStream(WebResponse webResponse)
        {
            Throw.IfNull(webResponse, nameof(webResponse));

            _logger.Info($"{nameof(ReadStream)} has been called with response with uri {webResponse.ResponseUri}.");

            var response = string.Empty;

            using (var dataStream = webResponse.GetResponseStream())
            {
                if (dataStream != null)
                {
                    var streamReader = new StreamReader(dataStream);

                    response = streamReader.ReadToEnd();
                }
            }

            return response;
        }
    }
}