using System.Collections.Generic;
using Feature.MultiLanguage;

namespace Adventure.Feature.MultiLanguage.Models
{
    public class LanguageSelectorViewModel
    {
        public string CurrentLanguageName { get; set; }

        public ILanguageSelectorSettings LanguageSelectorSettings { get; set; }
        
        public IEnumerable<LanguageViewModel> Languages { get; set; }
    }
}
