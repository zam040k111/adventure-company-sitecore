using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;

namespace Adventure.Feature.EventDetailsProvider.Utilities
{
    public class EventProviderSettingsReader : IEventProviderSettingsReader
    {
        private EventProviderSettings _settings;

        public EventProviderSettings Settings
        {
            get
            {
                if (_settings is null)
                {
                    _settings = ReadSettings();
                }

                return (EventProviderSettings)_settings.Clone();
            }
        }
        public string SearchIndex => Settings.SearchIndex;

        public string RequestUrl => Settings.RequestUrl;

        private EventProviderSettings ReadSettings()
        {
            var settings = new EventProviderSettings
            {
                RequestUrl = Sitecore.Configuration.Settings.GetSetting(EventProviderSettingsNames.RequestUrl),
                SearchIndex = Sitecore.Configuration.Settings.GetSetting(EventProviderSettingsNames.IndexName)
            };

            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.RequestUrl), nameof(settings.RequestUrl),
                Sitecore.Globalization.Translate.Text(DictionaryKeys.IncorrectSetting));
            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.SearchIndex), nameof(settings.SearchIndex),
                Sitecore.Globalization.Translate.Text(DictionaryKeys.IncorrectSetting));

            return settings;
        }
    }
}
