using Adventure.Feature.EventDetailsProvider.Models;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Stub for event data provider.
    /// </summary>
    public interface IRandomEventService
    {
        /// <summary>
        /// Generates new event randomly.
        /// </summary>
        /// <returns>An object of new event.</returns>
        NewEvent GenerateNewEvent();
    }
}
