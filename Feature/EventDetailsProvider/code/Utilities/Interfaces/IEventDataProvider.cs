using System.Collections.Generic;

namespace Adventure.Feature.EventDetailsProvider.Utilities.Interfaces
{
    /// <summary>
    /// Stub for real event data provider.
    /// </summary>
    public interface IEventDataProvider
    {
        /// <summary>
        /// Stub data.
        /// </summary>
        Dictionary<string, List<string>> Data { get; }
    }
}
