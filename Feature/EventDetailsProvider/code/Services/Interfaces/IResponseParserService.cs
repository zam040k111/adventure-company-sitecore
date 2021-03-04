using Adventure.Feature.EventDetailsProvider.Models;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Parse the response from event provider.
    /// </summary>
    public interface IResponseParserService
    {
        /// <summary>
        /// Parses response string into NewEvent object.
        /// </summary>
        /// <param name="responseContent">Response body from data provider.</param>
        /// <returns>New event object.</returns>
        NewEvent GetNewEventFromString(string responseContent);
    }
}
