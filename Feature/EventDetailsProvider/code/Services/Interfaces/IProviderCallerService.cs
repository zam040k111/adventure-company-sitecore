using System.Threading.Tasks;

namespace Adventure.Feature.EventDetailsProvider.Services.Interfaces
{
    /// <summary>
    /// Calls provider for data for new event.
    /// </summary>
    public interface IProviderCallerService
    {
        /// <summary>
        /// Gets data of new event from provider.
        /// </summary>
        /// <param name="requestUrl">Provider url.</param>
        /// <returns>Task which results with data from provider.</returns>
        Task<string> GetData(string requestUrl);
    }
}
