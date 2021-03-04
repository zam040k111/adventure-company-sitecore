using System;
using log4net.spi;
using log4net;
using Adventure.Foundation.DependencyInjection;
using log4net.Repository.Hierarchy;

namespace Adventure.Foundation.Logging.Logging
{
	[Service(typeof(ILog), Lifetime = Lifetime.Singleton)]
	public class LoggingService : ILog
	{
		public ILogger Logger { get; }

		public LoggingService(ILogger logger)
		{
			Logger = logger;
		}

		public LoggingService()
		{
			var hierarchy = (Hierarchy)LogManager.GetLoggerRepository();
			Logger = hierarchy.GetLogger("Develop");
		}

		public void Debug(object message)
		{
			Logger.Log(string.Empty, Level.DEBUG, message, null);
		}

		public void Debug(object message, Exception t)
		{
			Logger.Log(string.Empty, Level.DEBUG, message, t);
		}

		public void Info(object message)
		{
			Logger.Log(string.Empty, Level.INFO, message, null);
		}

		public void Info(object message, Exception t)
		{
			Logger.Log(string.Empty, Level.INFO, message, t);
		}

		public void Warn(object message)
		{
			Logger.Log(string.Empty, Level.WARN, message, null);
		}

		public void Warn(object message, Exception t)
		{
			Logger.Log(string.Empty, Level.WARN, message, t);
		}

		public void Error(object message)
		{
			Logger.Log(string.Empty, Level.ERROR, message, null);
		}

		public void Error(object message, Exception t)
		{
			Logger.Log(string.Empty, Level.ERROR, message, t);
		}

		public void Fatal(object message)
		{
			Logger.Log(string.Empty, Level.FATAL, message, null);
		}

		public void Fatal(object message, Exception t)
		{
			Logger.Log(string.Empty, Level.FATAL, message, t);
		}

		public bool IsDebugEnabled { get; } = true;
		public bool IsInfoEnabled { get; } = true;
		public bool IsWarnEnabled { get; } = true;
		public bool IsErrorEnabled { get; } = true;
		public bool IsFatalEnabled { get; } = true;
	}
}