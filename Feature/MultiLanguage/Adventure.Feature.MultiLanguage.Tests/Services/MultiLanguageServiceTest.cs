using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Adventure.Feature.MultiLanguage.Models;
using Adventure.Feature.MultiLanguage.Services;
using Feature.MultiLanguage;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Moq;
using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Links.UrlBuilders;
using Xunit;

namespace Adventure.Feature.MultiLanguage.Tests.Services
{
    public class MultiLanguageServiceTest
    {
        private readonly MultiLanguageService _multiLanguageService;
        private readonly Mock<BaseLanguageManager> _languageManager;
        private readonly Mock<BaseLinkManager> _linkManager;
        private readonly Mock<IMvcContext> _mvcContext;

        public MultiLanguageServiceTest()
        {
            _languageManager = new Mock<BaseLanguageManager>();
            _linkManager = new Mock<BaseLinkManager>();
            _mvcContext = new Mock<IMvcContext>();

            _multiLanguageService = new MultiLanguageService(
                Mock.Of<ILog>(),
                _mvcContext.Object,
                _languageManager.Object,
                _linkManager.Object);
        }

        [Theory]
        [MemberData(nameof(NullData))]
        public void GetViewModel_InputParameterNull_ShouldThrowArgumentNullException(Language language, Item item, Database database)
        {
            Assert.Throws<ArgumentNullException>
                (() => _multiLanguageService.GetViewModel(language, item, database));
        }

        [Theory]
        [MemberData(nameof(NoDataSourceData))]
        public void GetViewModel_NoDataSource_ShouldThrowArgumentException(Language language, Item item, Database database)
        {
            _mvcContext
                .Setup(x => x.GetDataSourceItem<ILanguageSelectorSettings>())
                .Returns((ILanguageSelectorSettings)null);

            Assert.Throws<ArgumentException>(() => _multiLanguageService.GetViewModel(language, item, database));
        }

        [Theory]
        [MemberData(nameof(CorrectData))]
        public void GetViewModel_ShouldReturnProperResult(
            Language language,
            Item item,
            Database database,
            LanguageCollection languages,
            ILanguageSelectorSettings dataSource,
            LanguageSelectorViewModel expectedResult)
        {
            _mvcContext
                .Setup(x => x.GetDataSourceItem<ILanguageSelectorSettings>())
                .Returns(dataSource);
            _languageManager
                .Setup(x=>x.GetLanguages(database))
                .Returns(languages);
            _linkManager
                .Setup(x=>x.GetItemUrl(It.IsAny<Item>(), It.IsAny<ItemUrlBuilderOptions>()))
                .Returns(expectedResult.Languages.First().Link);

            var result = _multiLanguageService.GetViewModel(language, item, database);

            Assert.Equal(expectedResult.CurrentLanguageName, result.CurrentLanguageName);
            Assert.Equal(expectedResult.Languages.First().DisplayName, result.Languages.First().DisplayName);
            Assert.Equal(expectedResult.Languages.First().Link, result.Languages.First().Link);
            Assert.Equal(expectedResult.LanguageSelectorSettings, dataSource);
        }

        public static IEnumerable<object[]> NullData()
        {
            var language = new Mock<Language>();
            var database = new Mock<Database>();
            var item = CreateItem();

            return new List<object[]>
            {
                new object[] { language.Object, item, null },
                new object[] { language.Object, null, database.Object },
                new object[] { null, item, database.Object }
            };
        }

        public static IEnumerable<object[]> CorrectData()
        {
            var language = new Mock<Language>();
            var database = new Mock<Database>();
            var item = CreateItem();
            var dataSource = new Mock<ILanguageSelectorSettings>();

            var cultureInfo = new CultureInfo("en");
            language.Setup(x=>x.CultureInfo).Returns(cultureInfo);

            var cultureInfoToList = new CultureInfo("ru-RU");
            var languageToList = new Mock<Language>();
            languageToList.Setup(x => x.CultureInfo).Returns(cultureInfoToList);

            var expectedResult = new LanguageSelectorViewModel
            {
                CurrentLanguageName = "English",
                LanguageSelectorSettings = dataSource.Object,
                Languages = new List<LanguageViewModel>
                {
                    new LanguageViewModel
                    {
                        Link = "/" + cultureInfoToList.Name + "/",
                        DisplayName = "Русский"
                    }
                }
            };

            return new List<object[]>
            {
                new object[]
                {
                    language.Object, item, database.Object,
                    new LanguageCollection { languageToList.Object },
                    dataSource.Object, expectedResult
                }
            };
        }

        public static IEnumerable<object[]> NoDataSourceData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Mock<Language>().Object,
                    CreateItem(),
                    new Mock<Database>().Object
                }
            };
        }

        private static Item CreateItem()
        {
            var item = new Mock<Item>(ID.NewID, ItemData.Empty, new Mock<Database>().Object);

            return item.Object;
        }
    }
}
