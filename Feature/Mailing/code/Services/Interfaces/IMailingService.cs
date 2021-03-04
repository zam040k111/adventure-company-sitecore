using Adventure.Feature.Mailing.Models;
using Feature.Mailing;

namespace Adventure.Feature.Mailing.Services.Interfaces
{
	public interface IMailingService
	{
		ISubscriptionForm GetSubscriptionForm();

		bool Subscribe(SubscribeViewModel subscribeViewModel);

		bool Unsubscribe(string email);

		void AsyncExecute();
	}
}
