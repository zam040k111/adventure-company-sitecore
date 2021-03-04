using Adventure.Feature.Registration.Services.Interfaces;
using Adventure.Foundation.Accounts.Providers.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DependencyInjection;
using log4net;

namespace Adventure.Feature.Registration.Services
{
    [Service(ServiceType = typeof(IRegistrationService), Lifetime = Lifetime.Scoped)]
    public class RegistrationService : IRegistrationService
    {
        private readonly IAdventureAccountsProvider _accountsProvider;
        private readonly ILog _logger;

        public RegistrationService(
            IAdventureAccountsProvider accountsProvider,
            ILog logger)
        {
            Throw.IfNull(accountsProvider, nameof(accountsProvider));
            Throw.IfNull(logger, nameof(logger));

            _accountsProvider = accountsProvider;
            _logger = logger;
        }

        public bool CreateUser(string userName, string email, string password)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(userName), nameof(userName), ArgumentNullOrWhiteSpaceMessage(nameof(userName)));
            Throw.IfCondition(string.IsNullOrWhiteSpace(email), nameof(email), ArgumentNullOrWhiteSpaceMessage(nameof(email)));
            Throw.IfCondition(string.IsNullOrWhiteSpace(password), nameof(password), ArgumentNullOrWhiteSpaceMessage(nameof(password)));

            _logger.Debug($"{nameof(CreateUser)} has been called with model" +
                          $"{userName} name, {email} email.");

            return _accountsProvider.CreateUser(userName, email, password);
        }

        public bool IsUserNameUnique(string userName)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(userName), nameof(userName), ArgumentNullOrWhiteSpaceMessage(nameof(userName)));

            _logger.Debug($"{nameof(IsUserNameUnique)} has been called with {userName} name.");

            return !_accountsProvider.UserExists(userName);
        }

        public bool IsEmailUnique(string email)
        {
            Throw.IfCondition(string.IsNullOrWhiteSpace(email), nameof(email), ArgumentNullOrWhiteSpaceMessage(nameof(email)));

            _logger.Debug($"{nameof(IsEmailUnique)} has been called with {email} email.");

            return !_accountsProvider.EmailExists(email);
        }

        private string ArgumentNullOrWhiteSpaceMessage(string argument)
        {
            return $"{argument}argument is null or white space.";
        }
    }
}