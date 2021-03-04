using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adventure.Feature.Mailing.Constants;
using Adventure.Feature.Mailing.Models;
using Adventure.Feature.Mailing.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using Adventure.Foundation.EmailProvider.Services.Interfaces;
using Adventure.Foundation.SearchProvider.Repositories.Interfaces;
using Foundation.EmailProvider;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using log4net;

namespace Adventure.Feature.Mailing.Services
{
	public class EventSenderService : IEventSenderService
	{
		private readonly IMongoGenericRepository<Subscriber> _subscribersRepository;
		private readonly IMongoGenericRepository<EmailMongoEntity> _emailsRepository;
		private readonly ISearchRepository<EventDetailsPageSearchItem, IEventDetailsPage> _eventRepository;
		private readonly ISearchRepository<EventTagSearchItem, IEventTag> _tagsRepository;
		private readonly IEmailService _emailService;
		private readonly IEmailGenerator _emailGenerator;
		private readonly ILog _logger;

		public EventSenderService(
			IMongoGenericRepository<Subscriber> subscribersRepository,
			IMongoGenericRepository<EmailMongoEntity> emailsRepository,
			ISearchRepository<EventDetailsPageSearchItem, IEventDetailsPage> eventRepository,
			ISearchRepository<EventTagSearchItem, IEventTag> tagsRepository,
			IEmailService emailService,
			IEmailGenerator emailGenerator,
			ILog logger)
		{
			Throw.IfNull(subscribersRepository, nameof(subscribersRepository));
			Throw.IfNull(emailsRepository, nameof(emailsRepository));
			Throw.IfNull(eventRepository, nameof(eventRepository));
			Throw.IfNull(tagsRepository, nameof(tagsRepository));
			Throw.IfNull(emailService, nameof(emailService));
			Throw.IfNull(emailGenerator, nameof(emailGenerator));
			Throw.IfNull(logger, nameof(logger));

			_subscribersRepository = subscribersRepository;
			_emailsRepository = emailsRepository;
			_eventRepository = eventRepository;
			_tagsRepository = tagsRepository;
			_emailService = emailService;
			_emailGenerator = emailGenerator;
			_logger = logger;
		}

		public async Task AsyncExecute()
		{
			var subscribers = _subscribersRepository.GetAll(i => i.IsSubscribed);

			foreach (var subscriber in subscribers)
			{
				var eventsToSend = GetEventsToSend(subscriber);
				if (eventsToSend == null || !eventsToSend.Any())
				{
					continue;
				}

				var email = _emailGenerator.Generate(subscriber, eventsToSend);
				var emailMongoEntity = new EmailMongoEntity
				{
					Recipient = subscriber.Email,
					EventIds = eventsToSend.Select(i => i.Id.ToString("N")).ToList()
				};
				var isSent = await AsyncSendEmail(email, subscriber.Email, emailMongoEntity);
				if (isSent)
				{
					_logger.Info($"The email with subject: \"{email.Subject}\" has been sent to {subscriber.Email}");
				}
				else
				{
					_logger.Error($"Unsuccessful sending of an email with subject: \"{email.Subject}\"");
				}
			}
		}

		private List<IEventDetailsPage> GetEventsToSend(Subscriber subscriber)
		{
			var userTagIds = _tagsRepository
				.GetAll(EventTag.TemplateId, i => subscriber.Tags.Contains(i.TagName))
				.Select(i => i.Id)
				.ToList();

			var eventsForSubscriber = _eventRepository.GetAll(EventDetailsPage.TemplateId)
				.Where(i => i.Tags != null && i.Tags.Any(x => userTagIds.Contains(x.Id)))
				.ToList();

			var lastSentEmail = _emailsRepository.GetAll(
				i => i.Recipient == subscriber.Email,
				o => o.DateAdded).ToList().LastOrDefault();

			if (lastSentEmail != null)
			{
				var eventsToSend = eventsForSubscriber
					.Where(i => !lastSentEmail.EventIds.Contains(i.Id.ToString("N")))
					.ToList();

				if (eventsToSend.Any())
				{
					return eventsToSend.Take(MailingConstants.CountEventsToEmail).ToList();
				}
			}
			else
			{
				if (eventsForSubscriber.Any())
				{
					return eventsForSubscriber.Take(MailingConstants.CountEventsToEmail).ToList();
				}
			}

			return null;
		}

		private async Task<bool> AsyncSendEmail(IEmail email, string recipient, EmailMongoEntity emailMongoEntity)
		{
			_emailService.SetDataSource(email);
			var isSent = await _emailService.SendEmail(recipient);
			if (isSent)
			{
				_emailsRepository.Create(emailMongoEntity);
			}

			return isSent;
		}
	}
}