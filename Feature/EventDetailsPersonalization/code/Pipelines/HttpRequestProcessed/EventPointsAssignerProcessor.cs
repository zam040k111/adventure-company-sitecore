using System.Collections.Generic;
using Adventure.Feature.EventDetailsPersonalization.Constants;
using Adventure.Foundation.Common.ValidationServices;
using Feature.EventDetailsPersonalization;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using Glass.Mapper.Sc;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Pipelines.HttpRequest;

namespace Adventure.Feature.EventDetailsPersonalization.Pipelines.HttpRequestProcessed
{
    public class EventPointsAssignerProcessor
    {
        private readonly ISitecoreService _sitecoreService;

        public EventPointsAssignerProcessor(ISitecoreService sitecoreService)
        {
            Throw.IfNull(sitecoreService, nameof(sitecoreService));

            _sitecoreService = sitecoreService;
        }

        public void Process(HttpRequestArgs args)
        {
            var item = Context.Item;
            if (item != null 
                && item.TemplateID == EventDetailsPage.TemplateId 
                && Tracker.Current != null)
            {
                var eventItem = _sitecoreService.GetItem<IEventDetailsPage>(item);

                var profileItem = Context.Database.GetItem(new ID(ItemIds.PreferableEventProfileItemId));
                var datasourceItem = Context.Database.GetItem(new ID(ItemIds.DatasourceItemId));

                var preferableEventTypeProfile = Tracker.Current.Interaction.Profiles[profileItem.Name];
                var eventPersonalizationSettings = _sitecoreService.GetItem<IEventPersonalizationSettings>(datasourceItem);

                var scores = new Dictionary<string, double>();
                foreach (var tag in eventItem.Tags)
                {
                    scores.Add(tag.TagName, eventPersonalizationSettings.PointPerVisit);
                }

                preferableEventTypeProfile.Score(scores);
                preferableEventTypeProfile.UpdatePattern();
            }
        }
    }
}
