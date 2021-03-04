using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DependencyInjection;
using Adventure.Foundation.Infrastructure.EmailSettings.Interfaces;
using Adventure.Foundation.EmailProvider.Constants;
using Adventure.Foundation.EmailProvider.Services.Interfaces;
using Foundation.EmailProvider;
using log4net;

namespace Adventure.Foundation.EmailProvider.Services
{
	[Service(ServiceType = typeof(IEmailService), Lifetime = Lifetime.Scoped)]
	public class EmailService : IEmailService
	{
        private const int TimeoutForConnecting = 3000;

        private readonly ILog _logger;
        private readonly IEmailSettingsReader _settingsReader;

        private IEmail _dataSource;

        public EmailService(
            ILog logger,
            IEmailSettingsReader settingsReader)
        {
            _logger = logger;
            _settingsReader = settingsReader;
        }

		public void SetDataSource(IEmail dataSource)
		{
			Throw.IfNull(dataSource, nameof(dataSource));

			_logger.Debug($"{nameof(SetDataSource)} has been called with not null data source.");

			_dataSource = dataSource;
		}

		public async Task<bool> SendEmail(string recipient)
        {
			Throw.IfNull(recipient, nameof(recipient));

			_logger.Debug($"{nameof(SendEmail)} has been called with recipient {recipient}.");

			var senderAddress = new MailAddress(_settingsReader.Email);
			var recipientAddress = new MailAddress(recipient);
			var email = new MailMessage(senderAddress, recipientAddress);

			if (_dataSource is null)
			{
				email.Subject = Sitecore.Globalization.Translate.Text(DictionaryKeys.EmailSubject);
				email.Body = Sitecore.Globalization.Translate.Text(DictionaryKeys.EmailBody);
			}
			else
			{
				email.Subject = _dataSource.Subject;
				email.Body = $"{_dataSource.Styles} {_dataSource.Header} {_dataSource.Body}";
				email.IsBodyHtml = true;
			}

			using (var client = new SmtpClient())
            {
				client.Host = _settingsReader.Host;
				client.Port = _settingsReader.Port;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential(
					_settingsReader.Email,
					_settingsReader.Password);
				client.EnableSsl = true;

				var sendingEmailTask = client.SendMailAsync(email);
				if (sendingEmailTask == await Task.WhenAny(sendingEmailTask, Task.Delay(TimeoutForConnecting)))
				{
					await sendingEmailTask;

					_logger.Info($"Welcoming email has been sent in {nameof(SendEmail)}.");

					return true;
				}

				_logger.Error($"Welcoming email has not been sent in {nameof(SendEmail)}.");

				client.SendAsyncCancel();

				return false;
            }
        }
	}
}