using Adventure.Feature.Navigation.Models;
using Adventure.Feature.Navigation.Utilities.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.Infrastructure.Constants;

namespace Adventure.Feature.Navigation.Utilities
{
    public class SearchSettingsReader : ISearchSettingsReader
    {
        private const string UnreadErrorMessage = "Settings has not been read.";

        private SearchSettings _settings;

        public SearchSettings Settings
        {
            get
            {
                if (_settings is null)
                {
                    _settings = ReadSettings();
                }

                return (SearchSettings)_settings.Clone();
            }
        }

        private SearchSettings ReadSettings()
        {
            var settings =  new SearchSettings
            {
                IndexName = Sitecore.Configuration.Settings.GetSetting(SearchItemsSettings.IndexName)
            };

            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.IndexName), nameof(settings.IndexName), UnreadErrorMessage);

            return settings;
        }
    }
}
