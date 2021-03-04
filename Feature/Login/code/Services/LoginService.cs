using System;
using System.Web;
using Adventure.Feature.Login.Constants;
using Adventure.Feature.Login.Models;
using Adventure.Feature.Login.Services.Interfaces;
using Adventure.Foundation.Accounts.Providers.Interfaces;
using Adventure.Foundation.Common.Helpers;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DependencyInjection;
using Feature.Login;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data;
using Sitecore.Links;

namespace Adventure.Feature.Login.Services
{
	[Service(ServiceType = typeof(ILoginService), Lifetime = Lifetime.Scoped)]
	public class LoginService : ILoginService
	{
		private readonly IAdventureAccountsProvider _accountsProvider;
		private readonly IMvcContext _context;

		public LoginService(IAdventureAccountsProvider accountsProvider, IMvcContext context)
		{
			Throw.IfNull(accountsProvider, nameof(accountsProvider));
			Throw.IfNull(context, nameof(context));
			_accountsProvider = accountsProvider;
			_context = context;
		}

		public ILoginPage GetLoginPage()
		{
			var page = _context.GetPageContextItem<ILoginPage>();
			Throw.IfNull(page, nameof(page));

			return page;
		}

		public string Login(LoginViewModel loginViewModel)
		{
			Throw.IfNullOrEmptyString(loginViewModel.UserName, nameof(loginViewModel.UserName));
			Throw.IfNullOrEmptyString(loginViewModel.Password, nameof(loginViewModel.Password));

			if (!_accountsProvider.UserExists(loginViewModel.UserName))
			{
				return TranslateHelper.TranslateMessage(DictionaryKeys.Messages.UserNotExists);
			}

			return _accountsProvider.Login(loginViewModel.UserName, loginViewModel.Password, loginViewModel.StillLoggedIn)
				? string.Empty 
				: TranslateHelper.TranslateMessage(DictionaryKeys.Messages.IncorrectPassword);
		}

		public string GetItemUrl(string itemId)
		{
			var localPath = HttpContext.Current.Request.UrlReferrer?.LocalPath;
			var pageName = !string.IsNullOrWhiteSpace(localPath)
				? localPath.Split('/').Length >= 3
					? localPath.Split('/')[2]
					: string.Empty
				: string.Empty;

			string headerName;
			try
			{
				headerName = Database.GetItem(new ItemUri(AccountHeader.Id))?.Name;
			}
			catch
			{
				headerName = string.Empty;
			}

			if (string.Equals(pageName, headerName))
			{
				return string.Empty;
			}

			var item = Database.GetItem(new ItemUri(itemId));
			if (item == null)
			{
				return string.Empty;
			}

			var url = LinkManager.GetItemUrl(item);

			return url;
		}

		public void Logout()
		{
			try
			{
				_accountsProvider.Logout();
			}
			catch (Exception e)
			{
				throw new Exception("Logout error. Something went wrong", e);
			}
		}
	}
}