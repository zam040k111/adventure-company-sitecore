using System.Collections.Generic;
using Adventure.Feature.Mailing.Models;
using Foundation.EmailProvider;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Mailing.Services.Interfaces
{
	public interface IEmailGenerator
	{
		IEmail Generate(Subscriber subscriber, List<IEventDetailsPage> eventsToSend);
	}
}
