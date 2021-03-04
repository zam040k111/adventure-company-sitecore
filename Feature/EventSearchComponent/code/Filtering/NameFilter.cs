using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class NameFilter : IFilter<EventSearchSettings, EventDetailsSearchItem>
	{
		public IQueryable<EventDetailsSearchItem> Execute(EventSearchSettings filterSettings, IQueryable<EventDetailsSearchItem> input)
		{
			if (!string.IsNullOrWhiteSpace(filterSettings.Name))
			{
				return input.Where(i => i.Title.Contains(filterSettings.Name));
			}

			return input;
        }
	}
}