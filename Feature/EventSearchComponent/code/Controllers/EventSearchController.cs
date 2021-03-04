using System.Collections.Generic;
using Adventure.Foundation.Common.Controllers;
using System.Web.Mvc;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Models;
using Adventure.Feature.EventSearchComponent.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Feature.EventSearchComponent;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;

namespace Adventure.Feature.EventSearchComponent.Controllers
{
    public class EventSearchController : BaseController
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;

        private readonly IEventSearchService _eventSearchService;
        private readonly IMvcContext _mvcContext;
        private readonly ILog _logger;

        public EventSearchController(
            IEventSearchService eventSearchService,
            IMvcContext mvcContext,
            ILog logger)
        {
            Throw.IfNull(eventSearchService, nameof(eventSearchService));
            Throw.IfNull(mvcContext, nameof(mvcContext));
            Throw.IfNull(logger, nameof(logger));

            _eventSearchService = eventSearchService;
            _mvcContext = mvcContext;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult SearchComponent()
        {
            _logger.Debug($"{nameof(SearchComponent)} has been called.");

            var dataSource = _mvcContext.GetDataSourceItem<IEventSearchSettings>();
            var model = new EventListViewModel
            {
                PageNumber = DefaultPageNumber,
                PageSize = DefaultPageSize,
                AllTags = _eventSearchService.GetAllTags()
            };

            model.PageSizeOptions = dataSource is null || !dataSource.PageSizeOptions.Any()
                ? new List<int> { model.PageSize } 
                : dataSource.PageSizeOptions.Select(x => x.Value);

            return ExecuteSafe(() => View(model));
        }

        [HttpPost]
        public JsonResult Search(EventSearchSettings searchSettings)
        {
            Throw.IfNull(searchSettings, nameof(searchSettings));

            _logger.Debug(
                $"{nameof(Search)} has been called with name {searchSettings.Name}," +
                $" difficulty {searchSettings.Difficulty}, starDate {searchSettings.StartDate}," +
                $" orderBy {searchSettings.OrderBy}, page number {searchSettings.PageNumber}," +
                $" page size {searchSettings.PageSize}.");

            return Json(_eventSearchService.Search(searchSettings));
        }
    }
}
