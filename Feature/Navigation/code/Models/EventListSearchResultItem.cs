using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Feature.Navigation.Models
{
    public class EventListSearchResultItem : SearchResultItem
    {
        [IndexField("excludefromnavigation")]
        public bool ExcludeFromNavigation { get; set; }
    }
}
