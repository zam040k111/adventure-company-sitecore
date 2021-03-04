using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Adventure.Feature.MultiLanguage.Constants;
using Adventure.Feature.MultiLanguage.Models;
using Adventure.Feature.MultiLanguage.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Feature.MultiLanguage;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;

namespace Adventure.Feature.MultiLanguage.Services
{
    public class MultiLanguageService : IMultiLanguageService
    {
        private const string DisplayNameRegexPattern = @":\s\w*";
        private const string NotMatchPatternMessage = "Input display name does not match display pattern.";

        private readonly ILog _logger;
        private readonly IMvcContext _mvcContext;
        private readonly BaseLanguageManager _languageManager;
        private readonly BaseLinkManager _linkManager;

        public MultiLanguageService(
            ILog logger,
            IMvcContext mvcContext,
            BaseLanguageManager languageManager,
            BaseLinkManager linkManager)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(mvcContext, nameof(mvcContext));
            Throw.IfNull(languageManager, nameof(languageManager));
            Throw.IfNull(linkManager, nameof(linkManager));

            _logger = logger;
            _mvcContext = mvcContext;
            _languageManager = languageManager;
            _linkManager = linkManager;
        }

        public LanguageSelectorViewModel GetViewModel(Language currentLanguage, Item currentItem, Database currentDatabase)
        {
            Throw.IfNull(currentLanguage, nameof(currentLanguage));
            Throw.IfNull(currentItem, nameof(currentItem));
            Throw.IfNull(currentDatabase, nameof(currentDatabase));

            var dataSource = _mvcContext.GetDataSourceItem<ILanguageSelectorSettings>();

            Throw.IfCondition(dataSource is null, nameof(dataSource),
                Translate.Text(DictionaryKeys.ErrorMessages.DataSourceNotExist));

            var languages = _languageManager.GetLanguages(currentDatabase);
            var viewModels = languages
                .Select(x =>
                {
                    var url = _linkManager.GetItemUrl(currentItem, new ItemUrlBuilderOptions
                    {
                        Language = x,
                        LanguageEmbedding = LanguageEmbedding.Always
                    });

                    return new LanguageViewModel
                    {
                        DisplayName = GetLanguageDisplayName(x.GetDisplayName()),
                        Link = url
                    };
                });

            return new LanguageSelectorViewModel
            {
                CurrentLanguageName = GetLanguageDisplayName(currentLanguage.GetDisplayName()),
                Languages = viewModels,
                LanguageSelectorSettings = dataSource
            };
        }

        private string GetLanguageDisplayName(string initialDisplayName)
        {
            Throw.IfNull(initialDisplayName, nameof(initialDisplayName));

            _logger.Debug($"{nameof(GetLanguageDisplayName)} has been called with display name {initialDisplayName}.");

            Throw.IfCondition(!Regex.IsMatch(initialDisplayName, DisplayNameRegexPattern), nameof(initialDisplayName), NotMatchPatternMessage);

            string regexResult = Regex.Match(initialDisplayName, DisplayNameRegexPattern).Value;
            var stringBuilder = new StringBuilder(regexResult);
            var clearName = stringBuilder.Replace(": ", string.Empty);
            clearName[0] = char.ToUpper(clearName[0]);

            return clearName.ToString();
        }
    }
}
