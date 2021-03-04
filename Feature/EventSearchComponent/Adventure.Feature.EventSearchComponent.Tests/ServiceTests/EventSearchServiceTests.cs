using System;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Models;
using Adventure.Feature.EventSearchComponent.Repositories.Interfaces;
using Adventure.Feature.EventSearchComponent.Services;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Adventure.Feature.EventSearchComponent.Tests.ServiceTests
{
	[TestClass]
	public class EventSearchServiceTests
	{
		private readonly Mock<IEventsRepository> _eventsRepository;
        private readonly Mock<IEventPipeline> _eventPipeline;
		private readonly EventSearchService _searchService;

        public EventSearchServiceTests()
		{
			_eventsRepository = new Mock<IEventsRepository>();
            _eventPipeline = new Mock<IEventPipeline>();
			_searchService = new EventSearchService(_eventsRepository.Object, _eventPipeline.Object, Mock.Of<ILog>());
        }

		[TestInitialize]
		public void TestInitialize()
		{
			_eventsRepository.Setup(i => i.SearchEvents(It.IsAny<IEventPipeline>(), It.IsAny<EventSearchSettings>()))
				.Returns(Enumerable.Empty<IEventDetailsPage>());

            _eventPipeline.Setup(i => i.Register(It.IsAny<IFilter<EventSearchSettings, EventDetailsSearchItem>>()))
                .Returns(_eventPipeline.Object);
        }

		[TestMethod]
		public void Search_WhenArgumentNull_ExpectArgumentNullException()
		{
            Assert.ThrowsException<ArgumentNullException>(() => _searchService.Search(null));
		}

		[TestMethod]
		public void Search_WhenArgumentNotNull_ExpectEventListViewModel()
		{
            var result = _searchService.Search(new EventSearchSettings());

			Assert.AreEqual(0, result.TotalItems);
		}
	}
}
