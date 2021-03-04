using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Utilities;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using log4net;
using Moq;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Utilities
{
    public class EventSourceReaderTest
    {
        private readonly EventSourceReader _eventSourceReader;

        public EventSourceReaderTest()
        {
            var eventDataProvider = new Mock<IEventDataProvider>();
            eventDataProvider
                .Setup(x => x.Data)
                .Returns(TestData());

            _eventSourceReader = new EventSourceReader(Mock.Of<ILog>(), eventDataProvider.Object);
        }

        [Fact]
        public void ReadSourceSectionValue_MustReadCorrectSection()
        {
            var data = TestData();
            var expectedResult = string.Join(
                " ", 
                data.Values.Select(
                    x => string.Join(
                        string.Empty,
                        x.Select(y => y))));

            var result = _eventSourceReader.ReadSourceSectionValue("A");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ReadSourceSectionValue_SectionNotExist_ThrowException()
        {
            Assert.Throws<Exception>(() => _eventSourceReader.ReadSourceSectionValue("B"));
        }

        private Dictionary<string, List<string>> TestData()
        {
            return new Dictionary<string, List<string>>
            {
                {
                    $"A{EventSourceSections.Beginning}", new List<string>
                    {
                        "B"
                    }
                },
                {
                    $"A{EventSourceSections.Center}", new List<string>
                    {
                        "A"
                    }
                },
                {
                    $"A{EventSourceSections.Ending}", new List<string>
                    {
                        "V"
                    }
                }
            };
        }
    }
}
