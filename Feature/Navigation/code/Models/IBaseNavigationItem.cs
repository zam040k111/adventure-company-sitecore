using Foundation.ORM.Sitecore.templates.Project.Adventure.Base;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Adventure.Feature.Navigation.Models
{
    public interface IBaseNavigationItem : IBaseNavigation
    {
        /// <summary>
        /// Returns the Parent of this Item
        /// </summary>
        /// <returns>
        /// Parent of this Item
        /// </returns>
        [SitecoreParent]
        IBaseNavigationItem Parent { get; set; }


        [SitecoreChildren]
        IEnumerable<IBaseNavigationItem> Children { get; set; }
    }
}
