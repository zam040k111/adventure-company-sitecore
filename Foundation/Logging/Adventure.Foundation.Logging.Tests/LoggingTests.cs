using System;
using Adventure.Foundation.Logging.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.spi;
using Moq;

namespace Adventure.Foundation.Logging.Tests
{
	[TestClass]
	public class LoggingTests
	{
		private readonly Mock<ILogger> _logger;
		private readonly string _message = "test message";

		public LoggingTests()
		{
			_logger = new Mock<ILogger>();
			_logger.Setup(i => i.Log(
				string.Empty,
				It.IsAny<Level>(),
				It.IsAny<string>(),
				It.IsAny<Exception>()));
		}

		[TestMethod]
		public void WriteLogDebug()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Debug(_message);

			_logger.Verify(mock => mock.Log(string.Empty, Level.DEBUG, _message, null), Times.Once);
		}

		[TestMethod]
		public void WriteLogDebugWithException()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Debug(_message, new NullReferenceException());

			_logger.Verify(mock => mock.Log(string.Empty, Level.DEBUG, _message, It.IsAny<Exception>()), Times.Once);
		}

		[TestMethod]
		public void WriteLogInfo()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Info(_message);

			_logger.Verify(mock => mock.Log(string.Empty, Level.INFO, _message, null), Times.Once);
		}

		[TestMethod]
		public void WriteLogInfoWithException()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Info(_message, new NullReferenceException());

			_logger.Verify(mock => mock.Log(string.Empty, Level.INFO, _message, It.IsAny<Exception>()), Times.Once);
		}

		[TestMethod]
		public void WriteLogWarn()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Warn(_message);

			_logger.Verify(mock => mock.Log(string.Empty, Level.WARN, _message, null), Times.Once);
		}

		[TestMethod]
		public void WriteLogWarnWithException()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Warn(_message, new NullReferenceException());

			_logger.Verify(mock => mock.Log(string.Empty, Level.WARN, _message, It.IsAny<Exception>()), Times.Once);
		}

		[TestMethod]
		public void WriteLogError()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Error(_message);

			_logger.Verify(mock => mock.Log(string.Empty, Level.ERROR, _message, null), Times.Once);
		}

		[TestMethod]
		public void WriteLogErrorWithException()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Error(_message, new NullReferenceException());

			_logger.Verify(mock => mock.Log(string.Empty, Level.ERROR, _message, It.IsAny<Exception>()), Times.Once);
		}

		[TestMethod]
		public void WriteLogFatal()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Fatal(_message);

			_logger.Verify(mock => mock.Log(string.Empty, Level.FATAL, _message, null), Times.Once);
		}

		[TestMethod]
		public void WriteLogFatalWithException()
		{
			// Arrange
			var logging = new LoggingService(_logger.Object);

			// Act
			logging.Fatal(_message, new NullReferenceException());

			_logger.Verify(mock => mock.Log(string.Empty, Level.FATAL, _message, It.IsAny<Exception>()), Times.Once);
		}
	}
}
