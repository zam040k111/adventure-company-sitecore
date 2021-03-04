using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class DateFilter : IFilter<EventSearchSettings, EventDetailsSearchItem>
	{
		public IQueryable<EventDetailsSearchItem> Execute(EventSearchSettings filterSettings, IQueryable<EventDetailsSearchItem> input)
		{
			if (filterSettings.StartDate != default)
			{
				return input.Where(i => i.StartDate >= filterSettings.StartDate);
			}

			return input;
		}
	}
}