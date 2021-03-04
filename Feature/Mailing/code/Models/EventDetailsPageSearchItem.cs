using System;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Feature.Mailing.Models
{
	public class EventDetailsPageSearchItem : SearchResultItem
	{

		[IndexField("Title")]
		public string Title { get; set; }

		[IndexField("StartDate")]
		public DateTime StartDate { get; set; }

		[IndexField("EndDate")]
		public DateTime EndDate { get; set; }

		[IndexField("Description")]
		public string Description { get; set; }

		[IndexField("ShortDescription")]
		public string ShortDescription { get; set; }

		[IndexField("Difficulty")]
		public int Difficulty { get; set; }

		[IndexField("tags")]
		public string[] TagsId { get; set; }
	}
}