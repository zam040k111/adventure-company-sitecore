using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.Mailing.Models;
using Adventure.Feature.Mailing.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Adventure.Foundation.SearchProvider.Repositories.Interfaces;
using Feature.Mailing;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Sitecore;

namespace Adventure.Feature.Mailing.Services
{
	public class MailingService : IMailingService
	{
		private readonly IMvcContext _context;
		private readonly IMongoGenericRepository<Subscriber> _subscribersRepository;
		private readonly ISearchRepository<EventTagSearchItem, IEventTag> _tagsRepository;
		private readonly IEventSenderService _eventSender;
		private readonly ILog _logger;
		private const string Tags = "Tags";

		public MailingService(
			IMvcContext context,
			IMongoGenericRepository<Subscriber> subscribersRepository,
			ISearchRepository<EventTagSearchItem, IEventTag> tagsRepository,
			IEventSenderService eventSender,
			ILog logger)
		{
			Throw.IfNull(context, nameof(context));
			Throw.IfNull(subscribersRepository, nameof(subscribersRepository));
			Throw.IfNull(tagsRepository, nameof(tagsRepository));
			Throw.IfNull(eventSender, nameof(eventSender));
			Throw.IfNull(logger, nameof(logger));

			_context = context;
			_subscribersRepository = subscribersRepository;
			_tagsRepository = tagsRepository;
			_eventSender = eventSender;
			_logger = logger;
		}

		public ISubscriptionForm GetSubscriptionForm()
		{
			var subscriptionForm = _context.GetDataSourceItem<ISubscriptionForm>();
			Throw.IfNull(subscriptionForm, nameof(subscriptionForm));

			return subscriptionForm;
		}

		public bool Subscribe(SubscribeViewModel subscribeViewModel)
		{
			Throw.IfNull(subscribeViewModel, nameof(subscribeViewModel));
			Throw.IfNull(subscribeViewModel.Email, nameof(subscribeViewModel.Email));

			List<string> tags;
			if (Context.User.Profile.Email == subscribeViewModel.Email)
			{
				var propertyValue = Context.User.Profile.GetCustomProperty(Tags);
				tags = string.IsNullOrWhiteSpace(propertyValue)
					? new List<string>()
					: Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(propertyValue);
			}
			else
			{
				tags = _tagsRepository.GetAll(EventTag.TemplateId).Select(i => i.TagName).ToList();
			}

			var subscriber = _subscribersRepository
				.Get(i => i.Email == subscribeViewModel.Email);

			if (subscriber == null)
			{
				_subscribersRepository.Create(new Subscriber
				{
					Email = subscribeViewModel.Email,
					Tags = tags
				});

				_logger.Info($"Created new subscriber {subscribeViewModel.Email}");
			}
			else
			{
				if (!subscriber.IsSubscribed)
				{
					_subscribersRepository.UpdateField(subscriber.Id, i => i.IsSubscribed, true);
					_logger.Info($"User {subscribeViewModel.Email} subscribed again");
				}

				_subscribersRepository.UpdateField(subscriber.Id, i => i.Tags, tags);
			}

			return true;
		}

		public bool Unsubscribe(string email)
		{
			Throw.IfNullOrEmptyString(email, nameof(email));

			var subscriber = _subscribersRepository.Get(i => i.Email == email);
			if (subscriber != null)
			{
				_subscribersRepository.UpdateField(subscriber.Id, i => i.IsSubscribed, false);
				_logger.Error($"User {email} unsubscribed");
				
				return true;
			}

			_logger.Error($"User {email} tried unsubscribe but record not found");

			return false;
		}

		public async void AsyncExecute()
		{
			await _eventSender.AsyncExecute();
		}
	}
}