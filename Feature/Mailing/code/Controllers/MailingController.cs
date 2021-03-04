using System.Linq;
using System.Web.Mvc;
using Adventure.Feature.Mailing.Models;
using Adventure.Feature.Mailing.Services.Interfaces;
using Adventure.Feature.Mailing.Validators;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;

namespace Adventure.Feature.Mailing.Controllers
{
	public class MailingController : BaseController
	{
		private readonly IMailingService _mailingService;

		public MailingController(IMailingService mailingService)
		{
			Throw.IfNull(mailingService, nameof(mailingService));

			_mailingService = mailingService;
		}

		[HttpGet]
		public ActionResult Subscription() => ExecuteSafe(() => View(_mailingService.GetSubscriptionForm()));

		[HttpPost]
		public JsonResult Subscription(
			SubscribeViewModel subscribeViewModel,
			SubscriptionValidator subscriptionValidator)
		{
			Throw.IfNull(subscribeViewModel, nameof(subscribeViewModel));
			Throw.IfNull(subscriptionValidator, nameof(subscriptionValidator));

			subscriptionValidator.SetMessageToEmailValidator(subscribeViewModel.EmailValidationMessage);
			var modelState = subscriptionValidator.Validate(subscribeViewModel);
			var success = false;
			var validationMessages = modelState.Errors.FirstOrDefault()?.ErrorMessage;
			if (modelState.IsValid)
			{
				success = _mailingService.Subscribe(subscribeViewModel);
			}

			return Json(new { success, validationMessages });
		}

		[HttpGet]
		public ActionResult Unsubscribe(string email) 
			=> ExecuteSafe(() => View(_mailingService.Unsubscribe(email)));
	}
}