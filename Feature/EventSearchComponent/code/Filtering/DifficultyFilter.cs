using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class DifficultyFilter : IFilter<EventSearchSettings, EventDetailsSearchItem>
	{
		public IQueryable<EventDetailsSearchItem> Execute(EventSearchSettings filterSettings, IQueryable<EventDetailsSearchItem> input)
		{
			if (filterSettings.Difficulty.HasValue)
			{
				return input.Where(i => i.Difficulty == filterSettings.Difficulty);
			}

			return input;
		}
	}
}