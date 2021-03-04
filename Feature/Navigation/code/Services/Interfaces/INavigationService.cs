using Adventure.Feature.Navigation.Models;
using Feature.Navigation;
using Sitecore.Data.Items;

namespace Adventure.Feature.Navigation.Services.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// Returns the Navigation hierarchy of Items from Current Page to Home Page
        /// </summary>
        /// <returns>
        /// IEnumerable of Items from Current Page to Home Page
        /// </returns>
        BreadcrumbNavigationViewModel GetBreadcrumb();

        NavigationComponentViewModel GetMenu(Item contextItem, INavigationComponentSettings navigationComponentSettings);
    }
}