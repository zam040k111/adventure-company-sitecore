using System.Collections.Generic;

namespace Adventure.Feature.Navigation.Models
{
    public class BreadcrumbNavigationViewModel
    {
        public IEnumerable<IBaseNavigationItem> NavItems { get; set; }
    }
}