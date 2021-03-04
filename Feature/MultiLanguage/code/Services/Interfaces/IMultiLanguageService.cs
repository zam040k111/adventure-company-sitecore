using Adventure.Feature.MultiLanguage.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Adventure.Feature.MultiLanguage.Services.Interfaces
{
    /// <summary>
    /// Provides functionality for working with languages features.
    /// </summary>
    public interface IMultiLanguageService
    {
        /// <summary>
        /// Gets view model for a language selector.
        /// </summary>
        /// <param name="currentLanguage">Language of a context.</param>
        /// <param name="currentItem">Context item.</param>
        /// <param name="currentDatabase">Database which is used currently.</param>
        /// <returns>Language selector view model object. Contains all non chosen languages in Languages IEnumerable.</returns>
        LanguageSelectorViewModel GetViewModel(Language currentLanguage, Item currentItem, Database currentDatabase);
    }
}
