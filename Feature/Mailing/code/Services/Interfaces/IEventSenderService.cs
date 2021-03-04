using System.Threading.Tasks;

namespace Adventure.Feature.Mailing.Services.Interfaces
{
	public interface IEventSenderService
	{
		Task AsyncExecute();
	}
}
