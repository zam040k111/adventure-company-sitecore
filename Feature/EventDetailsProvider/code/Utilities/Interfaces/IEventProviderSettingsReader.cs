using Adventure.Feature.EventDetailsProvider.Models;

namespace Adventure.Feature.EventDetailsProvider.Utilities.Interfaces
{
    /// <summary>
    /// Settings for event data provider module.
    /// </summary>
    public interface IEventProviderSettingsReader
    {
        /// <summary>
        /// Whole object.
        /// </summary>
        EventProviderSettings Settings { get; }

        /// <summary>
        /// Search index where to search for event lists.
        /// </summary>
        string SearchIndex { get; }

        /// <summary>
        /// Url of event data provider.
        /// </summary>
        string RequestUrl { get; }
    }
}