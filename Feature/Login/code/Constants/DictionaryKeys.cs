namespace Adventure.Feature.Login.Constants
{
	public static class DictionaryKeys
	{
		public static class Labels
		{
			public static class HeaderButtons
			{
				public const string Login = "Feature.Login.Labels.HeaderButtons.Login";
				public const string Logout = "Feature.Login.Labels.HeaderButtons.Logout";
				public const string HideForm = "Feature.Login.Labels.HeaderButtons.HideForm";
			}

			public const string UserName = "Feature.Login.Labels.UserName";
			public const string Password = "Feature.Login.Labels.Password";
			public const string StillLoggedIn = "Feature.Login.Labels.StillLoggedIn";
			public const string SubmitButtonText = "Feature.Login.Labels.SubmitButtonText";
		}
		public static class Messages
		{
			public const string IncorrectPassword = "Feature.Login.Messages.IncorrectPassword";
			public const string UserNotExists = "Feature.Login.Messages.UserNotExists";
		}

		public static class AfterLogin
		{
			public const string RedirectTo = "Feature.Login.AfterLogin.RedirectTo";
		}
	}
}