using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Feature.UserProfile.Models
{
    public class EventTagSearchItem : SearchResultItem
    {
        [IndexField("Tag Name")]
        public string TagName { get; set; }
    }
}
