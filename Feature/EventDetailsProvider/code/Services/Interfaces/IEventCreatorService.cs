using System.Threading.Tasks;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Higher class which perfoms whole proccess of creating new event for site.
    /// </summary>
    public interface IEventCreatorService
    {
        /// <summary>
        /// Creates event in sitecore tree.
        /// </summary>
        /// <returns></returns>
        Task CreateEvent();
    }
}
