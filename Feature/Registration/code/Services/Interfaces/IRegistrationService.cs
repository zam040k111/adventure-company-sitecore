namespace Adventure.Feature.Registration.Services.Interfaces
{
    public interface IRegistrationService
    {
        bool CreateUser(string userName, string email, string password);

        bool IsUserNameUnique(string userName);

        bool IsEmailUnique(string email);
    }
}
