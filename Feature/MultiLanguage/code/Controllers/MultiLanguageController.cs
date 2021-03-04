using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using log4net;
using System.Web.Mvc;
using Adventure.Feature.MultiLanguage.Services.Interfaces;
using Sitecore.Mvc.Presentation;

namespace Adventure.Feature.MultiLanguage.Controllers
{
    public class MultiLanguageController : BaseController
    {
        private readonly ILog _logger;
        private readonly IMultiLanguageService _multiLanguageService;

        public MultiLanguageController(
            ILog logger,
            IMultiLanguageService multiLanguageService)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(multiLanguageService, nameof(multiLanguageService));

            _logger = logger;
            _multiLanguageService = multiLanguageService;
        }

        [HttpGet]
        public ActionResult LanguageSelector()
        {
            _logger.Debug($"{nameof(LanguageSelector)} has been called.");

            var model = _multiLanguageService.GetViewModel(
                Sitecore.Context.Language,
                RenderingContext.Current.ContextItem,
                Sitecore.Context.Database);

            return ExecuteSafe(() => View(model));
        }
    }
}
