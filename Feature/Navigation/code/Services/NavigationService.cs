using System;
using Adventure.Feature.Navigation.Models;
using Adventure.Feature.Navigation.Services.Interfaces;
using Adventure.Foundation.Common.Exceptions;
using Adventure.Foundation.Common.ValidationServices;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Base.Constants;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.Navigation.Repositories.Interfaces;
using Feature.Navigation;
using Sitecore.Data.Items;
using static Adventure.Feature.Navigation.Constants.DictionaryKeys;

namespace Adventure.Feature.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IMvcContext _mvcContext;
        private readonly ILog _logger;
        private readonly IEventListRepository _eventListRepository;
        private readonly ID _eventDetailsTemplateId = new ID("{72B40E48-A2B6-4DDF-BE66-AD0911F53F75}");

        public NavigationService(
            IMvcContext mvcContext,
            ILog logger,
            IEventListRepository eventListRepository)
        {
            Throw.IfNull(mvcContext, nameof(mvcContext));
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(eventListRepository, nameof(eventListRepository));

            _mvcContext = mvcContext;
            _logger = logger;
            _eventListRepository = eventListRepository;
        }

        public BreadcrumbNavigationViewModel GetBreadcrumb()
        {
            _logger.Debug($"{nameof(GetBreadcrumb)} has been called.");

            var homePage = _mvcContext.GetRootItem<IBaseNavigationItem>();
            var currentItem = _mvcContext.GetPageContextItem<IBaseNavigationItem>();
            var navItems = new Stack<IBaseNavigationItem>();

            if (homePage is null)
            {
                _logger.Warn(
                    "[NavigationService.BreadcrumbNavigationViewModel] Can't create Breadcrumb - Home Page is null");

                throw new CannotDisplayComponentException(ExceptionMessages.HomePageIsNull);
            }

            if (currentItem is null)
            {
                _logger.Warn(
                    "[NavigationService.BreadcrumbNavigationViewModel] Can't create Breadcrumb - Current Page is null");

                throw new CannotDisplayComponentException(ExceptionMessages.CurrentPageIsNull);
            }

            while (currentItem.Id != homePage.Id)
            {
                if (currentItem.BaseTemplateIds.Select(x => new ID(x)).Contains(BaseNavigation.TemplateId)
                    && !currentItem.ExcludeFromNavigation)
                {
                    if (currentItem.Name.Equals("*"))
                    {
                        currentItem.Name = RenderingContext.Current.ContextItem.Name;
                        currentItem.Url = currentItem.Name;
                    }

                    navItems.Push(currentItem);
                }

                currentItem = currentItem.Parent;
            }

            if (!navItems.Any())
            {
                _logger.Warn(
                    "[NavigationService.BreadcrumbNavigationViewModel] Can't create Breadcrumb - Breadcrumb has no data");

                throw new CannotDisplayComponentException(ExceptionMessages.BreadcrumbHasNoData);
            }

            return new BreadcrumbNavigationViewModel
            {
                NavItems = navItems
            };
        }

        public NavigationComponentViewModel GetMenu(Item contextItem, INavigationComponentSettings navigationComponentSettings)
        {
            _logger.Debug($"{nameof(GetMenu)} has been called.");

            Throw.IfNull(contextItem, nameof(contextItem));

            var eventLists = _eventListRepository.GetEventLists().ToList();
            var title = navigationComponentSettings?.Title
                        ?? Sitecore.Globalization.Translate.Text(DefaultNavigationComponentSettings.Title);
            var currentEventListId = 
                eventLists.FirstOrDefault(x => x.Events.Any(y=>y.Id == contextItem.ID.Guid))?.Id
                ?? Guid.Empty;

            return new NavigationComponentViewModel
            {
                EventLists = eventLists,
                Title = title,
                CurrentEventList = currentEventListId,
                CurrentEventPage = contextItem.ID.Guid,
                IsEventDetailsPage = contextItem.TemplateID == _eventDetailsTemplateId
            };
        }
    }
}
