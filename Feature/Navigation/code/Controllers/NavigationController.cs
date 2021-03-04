using Adventure.Feature.Navigation.Services.Interfaces;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using System.Web.Mvc;
using Feature.Navigation;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Sitecore.Mvc.Presentation;

namespace Adventure.Feature.Navigation.Controllers
{
    public class NavigationController : BaseController
    {
        private readonly INavigationService _navigationService;
        private readonly ILog _logger;
        private readonly IMvcContext _mvcContext;

        public NavigationController(
            INavigationService navigationService,
            ILog logger,
            IMvcContext mvcContext)
        {
            Throw.IfNull(navigationService, nameof(navigationService));
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(mvcContext, nameof(mvcContext));

            _navigationService = navigationService;
            _logger = logger;
            _mvcContext = mvcContext;
        }

        public ActionResult Breadcrumb()
        {
            _logger.Debug($"{nameof(Breadcrumb)} has been called.");

            return ExecuteSafe(() => View(_navigationService.GetBreadcrumb()));
        }

        public ActionResult Menu()
        {
            _logger.Debug($"{nameof(Menu)} has been called.");

            var dataSource = _mvcContext.GetDataSourceItem<INavigationComponentSettings>();
            var contextItem = RenderingContext.Current.ContextItem;
            var navigationComponent = _navigationService.GetMenu(contextItem, dataSource);

            return ExecuteSafe(() => View(navigationComponent));
        }
    }
}