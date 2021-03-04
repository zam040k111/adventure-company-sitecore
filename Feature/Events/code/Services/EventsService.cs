using Adventure.Feature.Events.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Feature.Events;
using Feature.Events.Footer;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Linq;

namespace Adventure.Feature.Events.Services
{
    public class EventsService : IEventsService
    {
        private readonly IMvcContext _context;

        public EventsService(IMvcContext context)
        {
            Throw.IfNull(context, nameof(context));
            _context = context;
        }

        public ICarousel GetCarousel()
        {
            var carousel = _context.GetPageContextItem<ICarousel>();
            Throw.IfNull(carousel, nameof(carousel));
            Throw.IfEmpty(carousel.CarouselItems, nameof(carousel.CarouselItems));

            return carousel;
        }

        public IUpcomingEvents GetUpcomingEvents()
        {
            var upcomingEvents = _context.GetPageContextItem<IUpcomingEvents>();

            Throw.IfNull(upcomingEvents, nameof(upcomingEvents));

            var dateStart = upcomingEvents.StartDate == default
                ? DateTime.UtcNow
                : upcomingEvents.StartDate;

            upcomingEvents.Events = upcomingEvents.Events
                .Where(i => i.StartDate > dateStart)
                .Take(upcomingEvents.EventsCount)
                .OrderBy(i => i.StartDate);

            return upcomingEvents;
        }

        public IEventDetailsPage GetRichEvent()
        {
            var page = _context.GetPageContextItem<IEventDetailsPage>();
            Throw.IfNull(page, nameof(page));

            return page;
        }

        public IEventDetailsPage GetFeaturedEvent()
        {
            var page = _context.GetDataSourceItem<IEventDetailsPage>();
            Throw.IfNull(page, nameof(page));

            return page;
        }

        public IFooter GetFooter()
        {
            var page = _context.GetDataSourceItem<IFooter>();
            Throw.IfNull(page, nameof(page));

            return page;
        }

        public IGoogleMap GetMap()
        {
            var page = _context.GetDataSourceItem<IGoogleMap>();
            Throw.IfNull(page, nameof(page));

            return page;
        }
    }
}