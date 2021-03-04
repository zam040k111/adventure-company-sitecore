using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using System.Web.Mvc;
using Adventure.Feature.Events.Models;
using Adventure.Feature.Events.Services.Interfaces;
using Adventure.Foundation.Assets.Services.Interfaces;

namespace Adventure.Feature.Events.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventsService _eventsService;
        private readonly IRenderAssetsService _assetsService;

        public EventsController(IEventsService eventsService,
	        IRenderAssetsService assetsService)
        {
            Throw.IfNull(eventsService, nameof(eventsService));
            Throw.IfNull(assetsService, nameof(assetsService));

            _eventsService = eventsService;
            _assetsService = assetsService;
        }

        public ActionResult Carousel()
        {
            return ExecuteSafe(() => View(
	            new CarouselViewModel
	            {
		            Carousel = _eventsService.GetCarousel(),
		            AssetsService = _assetsService
	            }));
        }

        public ActionResult RichEvent()
        {
            return ExecuteSafe(() => View(_eventsService.GetRichEvent()));
        }

        public ActionResult UpcomingEvents()
        {
	        return ExecuteSafe(() => View(_eventsService.GetUpcomingEvents()));
        }

        public ActionResult FeaturedEvent()
        {
            return ExecuteSafe(() => View(_eventsService.GetFeaturedEvent()));
        }

        public ActionResult Footer()
        {
            return ExecuteSafe(() => View(_eventsService.GetFooter()));
        }

        public ActionResult Map()
        {
            return ExecuteSafe(() => View(_eventsService.GetMap()));
        }
    }
}