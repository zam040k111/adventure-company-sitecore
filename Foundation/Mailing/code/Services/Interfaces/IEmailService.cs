using System.Threading.Tasks;
using Foundation.EmailProvider;

namespace Adventure.Foundation.EmailProvider.Services.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendEmail(string recipient);

		void SetDataSource(IEmail dataSource);
	}
}
