using System;
using Sitecore.Mvc.Helpers;

namespace Adventure.Foundation.Common.Helpers
{
	public static class TranslateHelper
	{
		public static string TranslateLabel(this SitecoreHelper helper, string key)
		{
			return string.IsNullOrWhiteSpace(key) 
				? throw new ArgumentException("The key cannot be null or empty.", nameof(key))
				: Sitecore.Globalization.Translate.Text(key);
		}

		public static string TranslateMessage(string key)
		{
			return string.IsNullOrWhiteSpace(key)
				? throw new ArgumentException("The key cannot be null or empty.", nameof(key))
				: Sitecore.Globalization.Translate.Text(key);
		}
	}
}