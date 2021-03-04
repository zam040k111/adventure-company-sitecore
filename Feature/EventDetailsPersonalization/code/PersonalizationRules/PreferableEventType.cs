using Sitecore.Analytics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System.Linq;
using Adventure.Feature.EventDetailsPersonalization.Constants;
using Adventure.Foundation.Common.ValidationServices;
using Sitecore;
using Sitecore.Data;
using Sitecore.Marketing.xMgmt.Extensions;
using Glass.Mapper.Sc;
using Feature.EventDetailsPersonalization;

namespace Adventure.Feature.EventDetailsPersonalization.PersonalizationRules
{
    public class PreferableEventType<T> : OperatorCondition<T> where T : RuleContext
    {
        public string PreferableEventTypeKeyIds { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Throw.IfNull(PreferableEventTypeKeyIds, nameof(PreferableEventTypeKeyIds));

            var profileItem = Context.Database.GetItem(ItemIds.PreferableEventProfileItemId);
            var datasourceItem = Context.Database.GetItem(new ID(ItemIds.DatasourceItemId));
            var sitecoreService = new SitecoreService(Context.Database);

            var preferableEventTypeProfile = Tracker.Current.Interaction.Profiles[profileItem.Name];
            var eventPersonalizationSettings = sitecoreService.GetItem<IEventPersonalizationSettings>(datasourceItem); ;

            var idList = PreferableEventTypeKeyIds.Split('|');

            var profileKeys = idList.Select(x => Context.Database.GetItem(new ID(x)));

            if (preferableEventTypeProfile is null)
            {
                return false;
            }

            return profileKeys.All(y => preferableEventTypeProfile
                .Where(x => string.Equals(y.Name, x.Key,
                    System.StringComparison.InvariantCultureIgnoreCase))
                .Sum(x => x.Value) >= eventPersonalizationSettings.PointsForProfile);
        }
    }
}
