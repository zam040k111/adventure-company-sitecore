using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class TagFilter : IFilter<EventSearchSettings, EventDetailsSearchItem>
	{
		public IQueryable<EventDetailsSearchItem> Execute(EventSearchSettings filterSettings, IQueryable<EventDetailsSearchItem> input)
		{
			if (filterSettings.Tags != null && filterSettings.Tags.Length > 0)
			{
				var events = input.ToList();

				var ids = events
					.Where(i => i.TagsId != null && i.TagsId.Intersect(filterSettings.Tags).Count() == filterSettings.Tags.Length)
					.Select(i => i.ItemId)
					.ToList();

				if (!ids.Any())
				{
					return input.Where(i => false);
				}

				return input.Where(i => ids.Contains(i.ItemId));
			}

			return input;
		}
	}
}