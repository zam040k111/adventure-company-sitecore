using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class SortByFilter : IFilter<EventSearchSettings, EventDetailsSearchItem>
	{
		public IQueryable<EventDetailsSearchItem> Execute(EventSearchSettings filterSettings,
			IQueryable<EventDetailsSearchItem> input)
		{
			var methods = new Dictionary<string, Expression<Func<EventDetailsSearchItem, object>>>
			{
				{"name", i => i.Title},
				{"difficulty", i => i.Difficulty},
				{"date", i => i.StartDate}
			};

			return !string.IsNullOrWhiteSpace(filterSettings.OrderBy) && methods.ContainsKey(filterSettings.OrderBy)
				? input.OrderBy(methods[filterSettings.OrderBy])
				: input;
		}
	}
}