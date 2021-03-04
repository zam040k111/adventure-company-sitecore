using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;

namespace Adventure.Feature.Mailing.Models
{
    public class EventTagSearchItem : SearchResultItem
    {
        [IndexField("Tag Name")]
        public string TagName { get; set; }
    }
}
