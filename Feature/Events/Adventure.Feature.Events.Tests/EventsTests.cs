using Adventure.Feature.Events.Controllers;
using Feature.Events;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Adventure.Feature.Events.Tests
{
    [TestClass]
    public class EventsTests
    {
        private const string EmptyView = "EmptyView";
        private readonly Mock<IMvcContext> _context;
        private readonly Mock<ICarousel> _carousel;
        private readonly Mock<IEventDetailsPage> _eventDetails;

        public EventsTests()
        {
            _context = new Mock<IMvcContext>();
            _carousel = new Mock<ICarousel>();
            _eventDetails = new Mock<IEventDetailsPage>();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _carousel.Setup(i => i.CarouselItems).Returns(new List<IEventDetailsPage>());
            _context.Setup(i => i.GetPageContextItem<ICarousel>()).Returns(_carousel.Object);
            _context.Setup(i => i.GetPageContextItem<IEventDetailsPage>()).Returns(_eventDetails.Object);
        }

        [TestMethod]
        public void Carousel_WhenCarouselNull_ExpectEmptyView()
        {
            _context.Setup(i => i.GetPageContextItem<ICarousel>()).Returns((ICarousel)null);
            var controller = new EventsController(_context.Object);

            var result = controller.Carousel();

            Assert.AreEqual(EmptyView, ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void Carousel_WhenCarouselNotNullAndCarouselItemsIsEmpty_ExpectEmptyView()
        {
            var controller = new EventsController(_context.Object);

            var result = controller.Carousel();

            Assert.AreEqual(EmptyView, ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void Carousel_WhenCarouselNotNullAndCarouselItemsNotEmpty_ExpectViewResult()
        {
            _carousel.Setup(i => i.CarouselItems).Returns(new List<IEventDetailsPage> { null });
            var controller = new EventsController(_context.Object);

            var result = controller.Carousel();

            Assert.AreEqual(typeof(ViewResult), result.GetType());
        }

        [TestMethod]
        public void RichEvent_WhenPageNull_ExpectEmptyView()
        {
            _context.Setup(i => i.GetPageContextItem<IEventDetailsPage>()).Returns((IEventDetailsPage)null);
            var controller = new EventsController(_context.Object);

            var result = controller.RichEvent();

            Assert.AreEqual(EmptyView, ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void RichEvent_WhenPageNotNull_ExpectViewResult()
        {
            var controller = new EventsController(_context.Object);

            var result = controller.RichEvent();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
