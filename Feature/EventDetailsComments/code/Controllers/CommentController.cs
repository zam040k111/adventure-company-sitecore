using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Adventure.Feature.EventDetailsComments.Models;
using Adventure.Feature.EventDetailsComments.Services.Interfaces;
using Adventure.Feature.EventDetailsComments.Validators;
using Adventure.Foundation.Common.ValidationServices;
using Feature.EventDetailsComments;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;

namespace Adventure.Feature.EventDetailsComments.Controllers
{
    public class CommentController : SitecoreController
    {
        private const int DefaultDisplayCommentCount = 5;
        private readonly ICommentService _commentService;
        private readonly ILog _logger;
        private readonly IMvcContext _mvcContext;

        public CommentController(
            ICommentService commentService,
            ILog logger,
            IMvcContext mvcContext)
        {
            Throw.IfNull(commentService, nameof(commentService));
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(mvcContext, nameof(mvcContext));

            _commentService = commentService;
            _logger = logger;
            _mvcContext = mvcContext;
        }

        [HttpGet]
        public ActionResult CommentSection()
        {
            _logger.Debug($"{nameof(CommentSection)} has been called.");

            var dataSourceItem = _mvcContext.GetDataSourceItem<IComment>();

            Throw.IfNull(dataSourceItem, nameof(dataSourceItem));

            var model = new CommentSectionViewModel
            {
                EventId = Guid.Empty,
                CommentSettings = dataSourceItem
            };

            var context = RenderingContext.CurrentOrNull;
            if (!(context is null))
            {
                model.EventId = RenderingContext.Current.ContextItem.ID.Guid;
            }

            model.Comments = GetLasts(model.CommentSettings.DisplayedCommentCount, model.EventId);

            return View(model);
        }

        [HttpPost]
        public JsonResult Add(AddCommentViewModel addCommentViewModel, AddCommentValidator addCommentValidator)
        {
            Throw.IfNull(addCommentViewModel, nameof(addCommentViewModel));
            Throw.IfNull(addCommentValidator, nameof(addCommentValidator));

            var modelState = addCommentValidator.Validate(addCommentViewModel);
            var validation = new List<string>();
            if (modelState.IsValid)
            {
                _logger.Debug($"{nameof(Add)} has been called with valid model.");

                _commentService.Add(new Comment
                {
                    AuthorName = addCommentViewModel.AuthorName,
                    Text = addCommentViewModel.Text,
                    EventId = addCommentViewModel.EventId.GetValueOrDefault()
                });
            }
            else
            {
                foreach (var error in modelState.Errors)
                {
                    validation.Add(error.ErrorMessage);
                }

                _logger.Warn($"{nameof(Add)} has been called with invalid model.");
            }

            return Json(validation);
        }

        [HttpGet]
        public JsonResult GetDisplayed(Guid eventId, int commentsCountToDisplay = DefaultDisplayCommentCount)
        {
            _logger.Debug($"{nameof(GetDisplayed)} has been called.");

            return Json(
                GetLasts(commentsCountToDisplay, eventId)
                    .Select(x=>new
                    {
                        x.AuthorName,
                        x.Text,
                        DateAdded = x.DateAdded.ToString("dd MMM yyyy")
                    }),
                JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<CommentViewModel> GetLasts(int commentsCountToDisplay, Guid eventId)
        {
            if (commentsCountToDisplay < 1)
            {
                commentsCountToDisplay = DefaultDisplayCommentCount;
            }

            return _commentService.Get(commentsCountToDisplay, eventId);
        }
    }
}