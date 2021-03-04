using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Services;
using log4net;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class ResponseParserServiceTest
    {
        private readonly ResponseParserService _responseParserService;

        public ResponseParserServiceTest()
        {
            _responseParserService = new ResponseParserService(Mock.Of<ILog>());
        }

        [Fact]
        public void GetNewEventFromString_ShouldCreateCorrectEvent()
        {
            var expectedObject = new NewEvent { Description = "d", Difficulty = 2 };
            var serializedObject = JObject.FromObject(expectedObject);
            var serializedObjectString = serializedObject.ToString(Newtonsoft.Json.Formatting.None);

            var result = _responseParserService.GetNewEventFromString(serializedObjectString);

            Assert.Equal(expectedObject.Difficulty, result.Difficulty);
            Assert.Equal(expectedObject.Description, result.Description);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GetNewEventFromString_InputIncorrect_ShouldThrowArgumentException(string serializedObjectString)
        {
            Assert.Throws<ArgumentException>(() => _responseParserService.GetNewEventFromString(serializedObjectString));
        }
    }
}
