using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Adventure.Feature.EventDetailsProvider.Repositories.Interfaces
{
    /// <summary>
    /// Gives access to event lists in sitecore database.
    /// </summary>
    public interface IEventListRepository
    {
        /// <summary>
        /// Gets all event lists from sitecore database as Item objects.
        /// </summary>
        /// <returns>Collection of item objects.</returns>
        IEnumerable<Item> GetAll();
    }
}
