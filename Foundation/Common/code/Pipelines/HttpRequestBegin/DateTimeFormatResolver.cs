using System.Globalization;
using System.Threading;
using Sitecore.Pipelines.HttpRequest;

namespace Adventure.Foundation.Common.Pipelines.HttpRequestBegin
{
	public class DateTimeFormatResolver : HttpRequestProcessor
	{
		public override void Process(HttpRequestArgs args)
		{
			var culture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
			culture.DateTimeFormat.ShortDatePattern = "MMMM dd, yyyy";
			Thread.CurrentThread.CurrentCulture = culture;
		}
	}
}