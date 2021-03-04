using System.Web.Mvc;
using Adventure.Feature.Login.Models;
using Adventure.Feature.Login.Services.Interfaces;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using log4net;

namespace Adventure.Feature.Login.Controllers
{
	public class LoginController : BaseController
	{
		private readonly ILog _logger;
		private readonly ILoginService _loginService;

		public LoginController(ILog logger, ILoginService loginService)
		{
			Throw.IfNull(logger, nameof(logger));
			Throw.IfNull(loginService, nameof(loginService));
			_logger = logger;
			_loginService = loginService;
		}

		[HttpGet]
		public ActionResult LoginForm()
		{
			return ExecuteSafe(() => View(_loginService.GetLoginPage()));
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel loginViewModel)
		{
			Throw.IfNull(loginViewModel, nameof(loginViewModel));
			var message = _loginService.Login(loginViewModel);
			var redirect = _loginService.GetItemUrl(loginViewModel.AfterLoginRedirectTo);
			var success = string.IsNullOrWhiteSpace(message);

			_logger.Info(success
				? $"User named {loginViewModel.UserName} logged in successfully"
				: $"User named {loginViewModel.UserName} tried to log in unsuccessfully");

            Sitecore.Analytics.Tracker.Current.Session.IdentifyAs(
                Sitecore.Security.Accounts.User.Current.GetDomainName(),
                Sitecore.Security.Accounts.User.Current.Profile.Email);

            return Json(new { success, message, redirect });
		}

		[HttpGet]
		public ActionResult Logout()
		{
			return ExecuteSafe(() => {
				_loginService.Logout();
				return View("LoginForm");
			});
		}

		[HttpGet]
		public ActionResult LoginHeader() => View(model: GetUserName().Content);

		[HttpGet]
		public ContentResult GetUserName()
		{
			return Content(User.Identity.IsAuthenticated && !Sitecore.Context.User.IsAdministrator
				? User.Identity.Name.Split('\\')[1]
				: string.Empty);
		}
	}
}