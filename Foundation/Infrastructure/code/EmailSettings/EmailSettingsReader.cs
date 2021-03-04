using Adventure.Foundation.Infrastructure.Models;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DependencyInjection;
using Adventure.Foundation.Infrastructure.Constants;
using Adventure.Foundation.Infrastructure.EmailSettings.Interfaces;

namespace Adventure.Foundation.Infrastructure.EmailSettings
{
    [Service(ServiceType = typeof(IEmailSettingsReader), Lifetime = Lifetime.Scoped)]
    public class EmailSettingsReader : IEmailSettingsReader
    {
        private const string DefaultErrorMessage = "Incorrect or empty value.";

        private EmailSettingsModel _settings;

        public EmailSettingsModel Settings
        {
            get
            {
                if (_settings is null)
                {
                    _settings = GetSettings();
                }

                return (EmailSettingsModel)_settings.Clone();
            }
        }

        public string Email => Settings.Email;
        public string Password => Settings.Password;
        public int Port => Settings.Port;
        public string Host => Settings.Host;

        private EmailSettingsModel GetSettings()
        {
            var settings = new EmailSettingsModel
            {
                Email = Sitecore.Configuration.Settings.GetSetting(SmtpMailSettings.Email),
                Host = Sitecore.Configuration.Settings.GetSetting(SmtpMailSettings.Host),
                Password = Sitecore.Configuration.Settings.GetSetting(SmtpMailSettings.Password)
            };

            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.Email), nameof(settings.Email), DefaultErrorMessage);
            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.Host), nameof(settings.Host), DefaultErrorMessage);
            Throw.IfCondition(string.IsNullOrWhiteSpace(settings.Password), nameof(settings.Password), DefaultErrorMessage);

            var port = Sitecore.Configuration.Settings.GetSetting(SmtpMailSettings.Port);

            Throw.IfCondition(!int.TryParse(port, out var portIntValue), nameof(port), DefaultErrorMessage);

            settings.Port = portIntValue;

            return settings;
        }
    }
}
