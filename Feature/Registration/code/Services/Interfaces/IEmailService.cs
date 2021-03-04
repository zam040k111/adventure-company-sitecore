using System.Threading.Tasks;
using Feature.Registration;

namespace Adventure.Feature.Registration.Services.Interfaces
{
	public interface IEmailService
	{
		void SetDataSource(IWelcomingEmail dataSource);

		Task<bool> SendEmail(string recipient);
	}
}