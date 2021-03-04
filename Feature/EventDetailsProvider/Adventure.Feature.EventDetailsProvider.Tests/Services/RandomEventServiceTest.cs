using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using log4net;
using Moq;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class RandomEventServiceTest
    {
        private readonly RandomEventService _randomEventService;
        private readonly Mock<IEventSourceReader> _eventSourceReader;

        public RandomEventServiceTest()
        {
            _eventSourceReader = new Mock<IEventSourceReader>();

            _randomEventService = new RandomEventService(
                Mock.Of<ILog>(),
                _eventSourceReader.Object);
        }

        [Fact]
        public void GenerateEvent_ShouldCallProperMethodsAndReturnCorrectResult()
        {
            var expectedEvent = new NewEvent
            {
                Description = "d",
                ShortDescription = "sd",
                ButtonText = "bt",
                Title = "t"
            };

            _eventSourceReader
                .Setup(x => x.ReadSourceSectionValue(EventDetailsPage.Fields.ButtonText_FieldName))
                .Returns(expectedEvent.ButtonText);
            _eventSourceReader
                .Setup(x => x.ReadSourceSectionValue(EventDetailsPage.Fields.Description_FieldName))
                .Returns(expectedEvent.Description);
            _eventSourceReader
                .Setup(x => x.ReadSourceSectionValue(EventDetailsPage.Fields.ShortDescription_FieldName))
                .Returns(expectedEvent.ShortDescription);
            _eventSourceReader
                .Setup(x => x.ReadSourceSectionValue(EventDetailsPage.Fields.Title_FieldName))
                .Returns(expectedEvent.Title);

            var result = _randomEventService.GenerateNewEvent();

            Assert.Equal(expectedEvent.Title, result.Title);
            Assert.Equal(expectedEvent.ShortDescription, result.ShortDescription);
            Assert.Equal(expectedEvent.Description, result.Description);
            Assert.Equal(expectedEvent.ButtonText, result.ButtonText);
        }
    }
}
