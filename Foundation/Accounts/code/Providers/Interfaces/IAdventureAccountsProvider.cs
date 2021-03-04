namespace Adventure.Foundation.Accounts.Providers.Interfaces
{
    public interface IAdventureAccountsProvider
    {
        bool CreateUser(string userName, string email, string password);

        bool UserExists(string userName);

        bool Login(string userName, string password, bool persistent);

        void Logout();

        bool EmailExists(string email);
    }
}
