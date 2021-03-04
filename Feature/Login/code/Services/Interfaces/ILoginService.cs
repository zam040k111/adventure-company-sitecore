using Adventure.Feature.Login.Models;
using Feature.Login;

namespace Adventure.Feature.Login.Services.Interfaces
{
	public interface ILoginService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns>Throws ArgumentNullException if ILoginPage is not available in the current context otherwise returns instance of ILoginPage</returns>
		ILoginPage GetLoginPage();

		/// <summary>
		/// Method tries to login
		/// </summary>
		/// <param name="loginViewModel"></param>
		/// <returns>Empty string if the login was successful otherwise returns an appropriate message.</returns>
		string Login(LoginViewModel loginViewModel);

		/// <summary>
		/// Calls Logout method in AccountProvider
		/// </summary>
		void Logout();

		/// <summary>
		/// Method tries to get a redirection if needed.
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns>If user logged in with AccountHeader page method returns empty string otherwise returns url for redirecting</returns>
		string GetItemUrl(string itemId);
	}
}
