using System;
using Adventure.Feature.Login.Constants;
using Adventure.Feature.Login.Models;
using Adventure.Feature.Login.Services;
using Adventure.Foundation.Accounts.Providers.Interfaces;
using Feature.Login;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Adventure.Feature.Login.Tests.Services
{
	[TestClass]
	public class LoginTests
	{
		private readonly Mock<IAdventureAccountsProvider> _accountsProvider;
		private readonly Mock<IMvcContext> _context;
		private readonly Mock<ILoginPage> _loginPage;

		public LoginTests()
		{
			_accountsProvider = new Mock<IAdventureAccountsProvider>();
			_context = new Mock<IMvcContext>();
			_loginPage = new Mock<ILoginPage>();
		}

		[TestMethod]
		public void GetLoginPage_WhenLoginPageExist_ExpectLoginPage()
		{
			_context.Setup(i => i.GetPageContextItem<ILoginPage>()).Returns(_loginPage.Object);
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);

			var result = loginService.GetLoginPage();

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void GetLoginPage_WhenLoginPageNotExist_ExpectArgumentNullException()
		{
			_context.Setup(i => i.GetPageContextItem<ILoginPage>()).Returns((ILoginPage) null);
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);

			Assert.ThrowsException<ArgumentNullException>(() => loginService.GetLoginPage());
		}

		[TestMethod]
		public void Login_WhenUserNameIsNullOrEmpty_ExpectArgumentException()
		{
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);
			var loginViewModel = new LoginViewModel {Password = "123"};

			Assert.ThrowsException<ArgumentException>(() => loginService.Login(loginViewModel));
		}

		[TestMethod]
		public void Login_WhenPasswordIsNullOrEmpty_ExpectArgumentException()
		{
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);
			var loginViewModel = new LoginViewModel { UserName = "name" };

			Assert.ThrowsException<ArgumentException>(() => loginService.Login(loginViewModel));
		}

		[TestMethod]
		public void Login_WhenUserNotExist_ExpectAppropriateMessage()
		{
			_accountsProvider.Setup(i => i.UserExists(It.IsAny<string>())).Returns(false);
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);
			var loginViewModel = new LoginViewModel { UserName = "name", Password = "123"};

			var result = loginService.Login(loginViewModel);

			Assert.AreEqual(DictionaryKeys.Messages.UserNotExists, result);
		}

		[TestMethod]
		public void Login_WhenUserExistAndPasswordIsIncorrect_ExpectAppropriateMessage()
		{
			_accountsProvider.Setup(i => i.UserExists(It.IsAny<string>())).Returns(true);
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);
			var loginViewModel = new LoginViewModel { UserName = "name", Password = "123" };

			var result = loginService.Login(loginViewModel);

			Assert.AreEqual(DictionaryKeys.Messages.IncorrectPassword, result);
		}

		[TestMethod]
		public void Login_WhenUserExistAndPasswordIsCorrect_ExpectEmptyMessage()
		{
			_accountsProvider.Setup(i => i.UserExists(It.IsAny<string>())).Returns(true);
			_accountsProvider.Setup(i => i.Login(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<bool>())).Returns(true);

			var loginService = new LoginService(_accountsProvider.Object, _context.Object);
			var loginViewModel = new LoginViewModel { UserName = "name", Password = "123" };

			var result = loginService.Login(loginViewModel);

			Assert.AreEqual(string.Empty, result);
		}

		[TestMethod]
		public void Logout_ExpectCallLogoutMethod()
		{
			var loginService = new LoginService(_accountsProvider.Object, _context.Object);

			loginService.Logout();

			_accountsProvider.Verify(i => i.Logout());
		}
	}
}