using System.Net;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Reads stream from a web response.
    /// </summary>
    public interface IResponseDataStreamReaderService
    {
        /// <summary>
        /// Reads body web response into string.
        /// </summary>
        /// <param name="webResponse">Web response object.</param>
        /// <returns>Body read into string.</returns>
        string ReadStream(WebResponse webResponse);
    }
}
