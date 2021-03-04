using System.Threading.Tasks;
using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using log4net;
using Moq;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class EventCreatorServiceTest
    {
        private readonly EventCreatorService _eventCreatorService;
        private readonly Mock<IDatabaseWorkerService> _databaseWorkerService;
        private readonly Mock<IProviderCallerService> _providerCallerService;
        private readonly Mock<IResponseParserService> _responseParserService;
        private readonly Mock<IEventProviderSettingsReader> _eventProviderSettingsReader;

        public EventCreatorServiceTest()
        {
            _databaseWorkerService = new Mock<IDatabaseWorkerService>();
            _providerCallerService = new Mock<IProviderCallerService>();
            _responseParserService = new Mock<IResponseParserService>();
            _eventProviderSettingsReader = new Mock<IEventProviderSettingsReader>();

            _eventCreatorService = new EventCreatorService(
                _providerCallerService.Object,
                _responseParserService.Object,
                _databaseWorkerService.Object,
                _eventProviderSettingsReader.Object,
                Mock.Of<ILog>());
        }

        [Fact]
        public async Task CreateEvent_ShouldCallProperMethodsWithRightParameters()
        {
            var contentData = "data";
            var requestUrl = "url";
            var createdEvent = new NewEvent();

            _eventProviderSettingsReader
                .Setup(x => x.RequestUrl)
                .Returns(requestUrl);
            _providerCallerService
                .Setup(x => x.GetData(requestUrl))
                .ReturnsAsync(contentData);
            _responseParserService
                .Setup(x => x.GetNewEventFromString(contentData))
                .Returns(createdEvent);
            _databaseWorkerService.Setup(x => x.AddEvent(createdEvent));

            await _eventCreatorService.CreateEvent();

            _eventProviderSettingsReader.Verify(x => x.RequestUrl, Times.Once);
            _providerCallerService.Verify(x => x.GetData(requestUrl), Times.Once);
            _responseParserService.Verify(x => x.GetNewEventFromString(contentData), Times.Once);
            _databaseWorkerService.Verify(x => x.AddEvent(createdEvent), Times.Once);
        }
    }
}
