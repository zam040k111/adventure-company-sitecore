using Adventure.Foundation.Infrastructure.Models;

namespace Adventure.Foundation.Infrastructure.EmailSettings.Interfaces
{
    public interface IEmailSettingsReader
    {
        EmailSettingsModel Settings { get; }

        string Email { get; }

        string Password { get; }

        int Port { get; }

        string Host { get; }
    }
}
