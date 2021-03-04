using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Models;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.EventSearchComponent.Infrastructure
{
	public static class Mapper
    {
        private const string StartDateFormat = "G";

		public static IEnumerable<EventViewModel> ToViewModel(this IEnumerable<IEventDetailsPage> eventDetails)
		{
			return eventDetails
				.Select(x => new EventViewModel
				{
					Difficulty = x.Difficulty,
					StartDate = x.StartDate.ToString(StartDateFormat),
					Title = x.Title,
					Url = x.Url,
					Tags = x.Tags.Select(i=>i.TagName).ToArray()
				});
		}
	}
}