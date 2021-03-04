using System;
using System.Linq;
using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Repositories.Interfaces;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using log4net;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Adventure.Feature.EventDetailsProvider.Services
{
    public class DatabaseWorkerService : IDatabaseWorkerService
    {
        private readonly Random _random = new Random();
        private readonly TemplateID _eventTemplateId = new TemplateID(EventDetailsPage.TemplateId);
        private readonly ILog _logger;
        private readonly IEventListRepository _eventListRepository;

        public DatabaseWorkerService(
            ILog logger,
            IEventListRepository eventListRepository)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(eventListRepository, nameof(eventListRepository));

            _logger = logger;
            _eventListRepository = eventListRepository;
        }

        public void AddEvent(NewEvent newEvent)
        {
            Throw.IfNull(newEvent, nameof(newEvent));

            _logger.Debug($"{nameof(AddEvent)} has been called with event with title {newEvent.Title}.");

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                Item parent = GetRandomEventList();
                Item newEventItem = parent.Add($"{newEvent.Title} n{_random.Next()}", _eventTemplateId);

                newEventItem.Editing.BeginEdit();
                try
                {
                    newEventItem.Fields[EventDetailsPage.Fields.Description].Value = newEvent.Description;
                    newEventItem.Fields[EventDetailsPage.Fields.ButtonText].Value = newEvent.ButtonText;
                    newEventItem.Fields[EventDetailsPage.Fields.Difficulty].Value = $"{newEvent.Difficulty}";
                    newEventItem.Fields[EventDetailsPage.Fields.StartDate].Value = DateUtil.ToIsoDate(newEvent.StartDate);
                    newEventItem.Fields[EventDetailsPage.Fields.EndDate].Value = DateUtil.ToIsoDate(newEvent.EndDate);
                    newEventItem.Fields[EventDetailsPage.Fields.Title].Value = newEvent.Title;
                    newEventItem.Fields[EventDetailsPage.Fields.ShortDescription].Value = newEvent.ShortDescription;
                    newEventItem.Editing.EndEdit();
                }
                catch(Exception exception)
                {
                    _logger.Error(exception.Message);

                    newEventItem.Editing.CancelEdit();
                }
            }
        }

        private Item GetRandomEventList()
        {
            _logger.Debug($"{nameof(GetRandomEventList)} has been called.");

            var listOfLists = _eventListRepository.GetAll().ToList();
            var randomIndex = _random.Next(0, listOfLists.Count);

            return listOfLists[randomIndex];
        }
    }
}
