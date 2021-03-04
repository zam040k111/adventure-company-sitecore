using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services;
using log4net;
using Moq;
using System;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class SerializingServiceTest
    {
        private readonly SerializingService _serializingService;

        public SerializingServiceTest()
        {
            _serializingService = new SerializingService(Mock.Of<ILog>());
        }

        [Fact]
        public void SerializeEvent_ShouldSerializeEventToProperString()
        {
            var objectParameter = new NewEvent { ButtonText = "bt" };
            var expectedString = "{\"Title\":null,\"StartDate\":\"0001-01-01T00:00:00\",\"EndDate\":\"0001-01-01T00:00:00\",\"Description\":null,\"ShortDescription\":null,\"Difficulty\":0,\"ButtonText\":\"bt\"}";

            var result = _serializingService.SerializeEvent(objectParameter);

            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void SerializeEvent_InputNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _serializingService.SerializeEvent(null));
        }
    }
}
