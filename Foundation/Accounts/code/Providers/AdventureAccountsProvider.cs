using System;
using System.Linq;
using Adventure.Foundation.Accounts.Providers.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DependencyInjection;
using log4net;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace Adventure.Foundation.Accounts.Providers
{
    [Service(ServiceType = typeof(IAdventureAccountsProvider), Lifetime = Lifetime.Scoped)]
    public class AdventureAccountsProvider : UserProvider, IAdventureAccountsProvider
    {
        private readonly ILog _logger;

        public AdventureAccountsProvider(ILog logger)
        {
            _logger = logger;
        }

        public bool CreateUser(string userName, string email, string password)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(userName), nameof(userName), ArgumentNullOrWhiteSpaceMessage(nameof(userName)));
            Throw.IfCondition(string.IsNullOrWhiteSpace(email), nameof(email), ArgumentNullOrWhiteSpaceMessage(nameof(email)));
            Throw.IfCondition(string.IsNullOrWhiteSpace(password), nameof(password), ArgumentNullOrWhiteSpaceMessage(nameof(password)));

            try
            {
                var user = User.Create(Context.Domain.GetFullName(userName), password);
                user.Profile.Email = email;
                user.Profile.FullName = userName;
                user.Profile.Save();

                _logger.Info($"User with name {userName} was successfully registered.");
            }
            catch (Exception exception)
            {
                _logger.Error(
                    $"{exception.Message} while registering user with name {userName} in {nameof(CreateUser)}.");

                return false;
            }

            return true;
        }

        public bool UserExists(string userName)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(userName), nameof(userName), ArgumentNullOrWhiteSpaceMessage(nameof(userName)));

            _logger.Debug($"{nameof(UserExists)} has been called with user name {userName}.");

            return User.Exists(Context.Domain.GetFullName(userName));
        }

        public bool EmailExists(string email)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(email), nameof(email), ArgumentNullOrWhiteSpaceMessage(nameof(email)));

            _logger.Debug($"{nameof(EmailExists)} has been called with email {email}.");

            return GetUsersByEmail(0, 1, email).Any();
        }

        private string ArgumentNullOrWhiteSpaceMessage(string argument)
        {
            return $"{argument}argument is null or white space.";
        }

        public bool Login(string userName, string password, bool persistent = false)
        {
	        Assert.ArgumentCondition(!string.IsNullOrWhiteSpace(userName), nameof(userName), ArgumentNullOrWhiteSpaceMessage(nameof(userName)));
	        Assert.ArgumentCondition(!string.IsNullOrWhiteSpace(password), nameof(password), ArgumentNullOrWhiteSpaceMessage(nameof(password)));

	        var accountName = string.Empty;
            var domain = Context.Domain;

            if (domain != null)
            {
	            accountName = domain.GetFullName(userName);
            }
            
			var result = AuthenticationManager.Login(accountName, password, persistent);

            if (result)
            {
	            _logger.Info($"User with name {userName} logged in successfully.");

                return true;
            }

            _logger.Error($"User with name {userName} tried to login unsuccessfully.");

            return false;
        }

        public void Logout()
        {
	        AuthenticationManager.Logout();
        }
    }
}
