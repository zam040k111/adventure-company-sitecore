using Adventure.Feature.EventDetailsProvider.Models;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Serializes new event to string for sending.
    /// </summary>
    public interface ISerializingService
    {
        /// <summary>
        /// Serializes NewEvent object into string.
        /// </summary>
        /// <param name="newEvent">Object to be serialized.</param>
        /// <returns>String with data.</returns>
        string SerializeEvent(NewEvent newEvent);
    }
}
