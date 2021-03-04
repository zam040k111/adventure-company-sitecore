using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;
using Adventure.Foundation.Common.ValidationServices;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
	public class EventPipeline : Pipeline<EventSearchSettings, EventDetailsSearchItem>, IEventPipeline
    {
        private const string StandardValues = "__Standard Values";

        private readonly Sitecore.Data.ID _templateId = new Sitecore.Data.ID("{72B40E48-A2B6-4DDF-BE66-AD0911F53F75}");

        public override IQueryable<EventDetailsSearchItem> Process(
			EventSearchSettings filterSettings,
			IQueryable<EventDetailsSearchItem> input)
		{
			Throw.IfNull(filterSettings, nameof(filterSettings));
			Throw.IfNull(input, nameof(input));

            input = input.Where(i => i.TemplateId == _templateId && i.Name != StandardValues);
            Filters.ForEach(filter => input = filter.Execute(filterSettings, input));
			TotalItems = input.Count();

			return input.Skip((filterSettings.PageNumber - 1) * filterSettings.PageSize).Take(filterSettings.PageSize);
		}
	}
}