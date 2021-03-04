using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;

namespace Adventure.Feature.EventSearchComponent.Models
{
    public class EventTagSearchItem : SearchResultItem
    {
        [IndexField("Tag Name")]
        public string TagName { get; set; }
    }
}
