using System.Collections.Generic;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Navigation.Repositories.Interfaces
{
    public interface IEventListRepository
    {
        IEnumerable<IEventListPage> GetEventLists();
    }
}
