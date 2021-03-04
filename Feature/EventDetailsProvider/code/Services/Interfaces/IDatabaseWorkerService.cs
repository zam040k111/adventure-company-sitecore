using Adventure.Feature.EventDetailsProvider.Models;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Perfoms adding an event to the Sitecore database.
    /// </summary>
    public interface IDatabaseWorkerService
    {
        /// <summary>
        /// Translates NewEvent class into item and adds to a random event list.
        /// </summary>
        /// <param name="newEvent"></param>
        void AddEvent(NewEvent newEvent);
    }
}
