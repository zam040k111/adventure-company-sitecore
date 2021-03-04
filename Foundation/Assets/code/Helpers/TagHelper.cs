namespace Adventure.Foundation.Assets.Helpers
{
	public static class TagHelper
	{
		public static string Script(string source)
		{
			return $"<script src=\"{source}\" defer></script>";
		}

		public static string Style(string source)
		{
			return $"<link href=\"{source}\" rel=\"stylesheet\" />";
		}
	}
}