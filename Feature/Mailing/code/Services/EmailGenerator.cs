using System.Collections.Generic;
using System.Text;
using Adventure.Feature.Mailing.Constants;
using Adventure.Feature.Mailing.Models;
using Adventure.Feature.Mailing.Services.Interfaces;
using Foundation.EmailProvider;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Mailing.Services
{
	public class EmailGenerator : IEmailGenerator
	{
		public IEmail Generate(Subscriber subscriber, List<IEventDetailsPage> eventsToSend)
		{
			var subjectBuilder = new StringBuilder();
			var bodyBuilder = new StringBuilder();
			for (int i = 0; i < eventsToSend.Count; i++)
			{
				if (!string.IsNullOrWhiteSpace(eventsToSend[i].Title))
				{
					subjectBuilder.Append(i + 1 < eventsToSend.Count
						? $"{eventsToSend[i].Title}, "
						: $"{eventsToSend[i].Title}");
				}

				bodyBuilder.Append(
					$"<h4>Date start: {eventsToSend[i].StartDate}</h4><h4>Date end: {eventsToSend[i].EndDate}</h4>");
				if (eventsToSend[i].Image != null)
				{
					bodyBuilder.Append($"<img src=\"{eventsToSend[i].Image.Src}\"/>");
				}

				if (!string.IsNullOrWhiteSpace(eventsToSend[i].Description))
				{
					bodyBuilder.Append($"<div><h3>Description:</h3>{eventsToSend[i].Description}</div>");
				}
			}

			bodyBuilder.Append(
				$"</br></br><div>You can <a href=\"{MailingConstants.UnsubscribeUrl}{subscriber.Email}\">unsubscribe</a></div>");

			return new Email {Subject = subjectBuilder.ToString(), Body = bodyBuilder.ToString()};
		}
	}
}