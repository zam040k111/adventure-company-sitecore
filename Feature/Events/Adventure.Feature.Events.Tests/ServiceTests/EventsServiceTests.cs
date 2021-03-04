using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.Events.Services;
using Feature.Events;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Adventure.Feature.Events.Tests.ServiceTests
{
	[TestClass]
	public class EventsServiceTests
	{
		private readonly Mock<IMvcContext> _context;
		private readonly Mock<ICarousel> _carousel;
		private readonly Mock<IEventDetailsPage> _eventDetails;
		private readonly Mock<IUpcomingEvents> _upcomingEvents;

		public EventsServiceTests()
		{
			_context = new Mock<IMvcContext>();
			_carousel = new Mock<ICarousel>();
			_eventDetails = new Mock<IEventDetailsPage>();
			_upcomingEvents = new Mock<IUpcomingEvents>();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_carousel.Setup(i => i.CarouselItems).Returns(new List<IEventDetailsPage>());
			_context.Setup(i => i.GetPageContextItem<ICarousel>()).Returns(_carousel.Object);
			_context.Setup(i => i.GetPageContextItem<IEventDetailsPage>()).Returns(_eventDetails.Object);
			_context.Setup(i => i.GetPageContextItem<IUpcomingEvents>()).Returns(_upcomingEvents.Object);
		}

		[TestMethod]
		public void GetCarousel_WhenCarouselNull_ExpectArgumentNullException()
		{
			_context.Setup(i => i.GetPageContextItem<ICarousel>()).Returns((ICarousel) null);
			var eventsService = new EventsService(_context.Object);

			Assert.ThrowsException<ArgumentNullException>(() => eventsService.GetCarousel());
		}

		[TestMethod]
		public void GetCarousel_WhenCarouselEmpty_ExpectArgumentException()
		{
			var eventsService = new EventsService(_context.Object);

			Assert.ThrowsException<ArgumentException>(() => eventsService.GetCarousel());
		}

		[TestMethod]
		public void GetCarousel_WhenCarouselContainsCarouselItems_ExpectCarouselElement()
		{
			_carousel.Setup(i => i.CarouselItems).Returns(new List<IEventDetailsPage> {null});
			_context.Setup(i => i.GetPageContextItem<ICarousel>()).Returns(_carousel.Object);
			var eventsService = new EventsService(_context.Object);

			var result = eventsService.GetCarousel();

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void GetRichEvent_WhenRichEventNull_ExpectArgumentNullException()
		{
			_context.Setup(i => i.GetPageContextItem<IEventDetailsPage>()).Returns((IEventDetailsPage) null);
			var eventsService = new EventsService(_context.Object);

			Assert.ThrowsException<ArgumentNullException>(() => eventsService.GetRichEvent());
		}

		[TestMethod]
		public void GetRichEvent_WhenRichEventNotNull_ExpectRichEventElement()
		{
			var eventsService = new EventsService(_context.Object);

			var result = eventsService.GetRichEvent();

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void GetUpcomingEvents_WhenUpcomingEventsNull_ExpectArgumentNullException()
		{
			_context.Setup(i => i.GetPageContextItem<IUpcomingEvents>()).Returns((IUpcomingEvents) null);
			var eventsService = new EventsService(_context.Object);

			Assert.ThrowsException<ArgumentNullException>(() => eventsService.GetUpcomingEvents());
		}

		[TestMethod]
		public void GetUpcomingEvents_EventsFilteringByDate_ExpectFilteredEvents()
		{
			var eventDetails = new Mock<IEventDetailsPage>();
			eventDetails.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(2));
			_eventDetails.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(4));
			var events = new List<IEventDetailsPage> {_eventDetails.Object, eventDetails.Object};
			_upcomingEvents.Setup(i => i.EventsCount).Returns(2);
			_upcomingEvents.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(3));
			_upcomingEvents.SetupProperty(i => i.Events, events);
			_context.Setup(i => i.GetPageContextItem<IUpcomingEvents>()).Returns(_upcomingEvents.Object);
			var eventsService = new EventsService(_context.Object);

			var result = eventsService.GetUpcomingEvents();

			Assert.AreEqual(events.Count - 1, result.Events.Count());
		}

		[TestMethod]
		public void GetUpcomingEvents_EventsFilteringByCount_ExpectFilteredEvents()
		{
			const int unusedCount = 1;
			var eventDetails = new Mock<IEventDetailsPage>();
			eventDetails.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(2));
			_eventDetails.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(4));
			var events = new List<IEventDetailsPage> { _eventDetails.Object, eventDetails.Object };
			_upcomingEvents.Setup(i => i.EventsCount).Returns(events.Count - unusedCount);
			_upcomingEvents.Setup(i => i.StartDate).Returns(DateTime.UtcNow);
			_upcomingEvents.SetupProperty(i => i.Events, events);
			_context.Setup(i => i.GetPageContextItem<IUpcomingEvents>()).Returns(_upcomingEvents.Object);
			var eventsService = new EventsService(_context.Object);

			var result = eventsService.GetUpcomingEvents();

			Assert.AreEqual(events.Count - unusedCount, result.Events.Count());
		}

		[TestMethod]
		public void GetUpcomingEvents_EventsOrderByDate_ExpectOrderedEvents()
		{
			var eventDetails = new Mock<IEventDetailsPage>();
			var earliestDate = DateTime.UtcNow.AddDays(1);
			eventDetails.Setup(i => i.StartDate).Returns(earliestDate);
			_eventDetails.Setup(i => i.StartDate).Returns(DateTime.UtcNow.AddDays(2));
			var events = new List<IEventDetailsPage> { _eventDetails.Object, eventDetails.Object };
			_upcomingEvents.Setup(i => i.EventsCount).Returns(2);
			_upcomingEvents.Setup(i => i.StartDate).Returns(DateTime.UtcNow);
			_upcomingEvents.SetupProperty(i => i.Events, events);
			_context.Setup(i => i.GetPageContextItem<IUpcomingEvents>()).Returns(_upcomingEvents.Object);
			var eventsService = new EventsService(_context.Object);

			Assert.AreEqual(earliestDate, events[1].StartDate);

			var result = eventsService.GetUpcomingEvents();

			Assert.AreEqual(earliestDate, result.Events.ToList()[0].StartDate);
		}
	}
}
