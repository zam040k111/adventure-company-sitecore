namespace Adventure.Feature.Login.Models
{
	public class LoginViewModel
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public string AfterLoginRedirectTo { get; set; }

		public bool StillLoggedIn { get; set; }
	}
}