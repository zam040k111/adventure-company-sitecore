using Adventure.Feature.EventDetailsProvider.Services;
using log4net;
using Moq;
using System.IO;
using System.Net;
using Xunit;
using System.Text;
using System;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class DataStreamReaderServiceTest
    {
        private readonly ResponseDataStreamReaderService _dataStreamReaderService;
        
        public DataStreamReaderServiceTest()
        {
            _dataStreamReaderService = new ResponseDataStreamReaderService(Mock.Of<ILog>());
        }

        [Fact]
        public void ReadStream_ShouldReadWebResponseContent()
        {
            var content = "content";
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(content));
            var webResponse = new Mock<WebResponse>();
            webResponse
                .Setup(x => x.GetResponseStream())
                .Returns(stream);

            var result = _dataStreamReaderService.ReadStream(webResponse.Object);

            Assert.Equal(content, result);
        }

        [Fact]
        public void ReadStream_InputNull_ShouldThrowNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => _dataStreamReaderService.ReadStream(null));
        }
    }
}
