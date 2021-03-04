using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Adventure.Feature.UserProfile.Constants;
using Adventure.Feature.UserProfile.Models;
using Adventure.Feature.UserProfile.Services.Interfaces;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Sitecore;

namespace Adventure.Feature.UserProfile.Controllers
{
	public class UserProfileController : BaseController
	{
		private readonly IUserProfileService _userProfileService;

		public UserProfileController(IUserProfileService userProfileService)
		{
			Throw.IfNull(userProfileService, nameof(userProfileService));

			_userProfileService = userProfileService;
		}

		[HttpGet]
		public ActionResult ProfilePage()
		{
			if (User.Identity.IsAuthenticated && !Context.User.IsAdministrator)
			{
				return View(_userProfileService.GetUserProfile());
			}

			return EmptyView("Access error");
		}

		[HttpPost]
		public ActionResult ProfilePage(UserProfileModel userProfile, Validator.Validator validator)
		{
			var modelState = validator.Validate(userProfile);
			var validation = new List<string>();
			var success = false;
			if (modelState.IsValid)
			{
				success = _userProfileService.SetUserProfile(userProfile);
				if (!success)
				{
					validation.Add(DictionaryKeys.Messages.InvalidOldPassword);
				}
			}
			else
			{
				modelState.Errors.ForEach(err => validation.Add(err.ErrorMessage));
			}

			return Json(new { validation, success });
        }

		[HttpPost]
		public ActionResult UploadPhoto(HttpPostedFileBase file, Validator.ImageValidator validator)
		{
			var validation = new List<string>();
			var success = false;
			if (file == null)
			{
				validation.Add(DictionaryKeys.Messages.NoDataToUpload);

				return Json(new { validation, success });
			}

			var modelState = validator.Validate(file);
			if (modelState.IsValid)
			{
				success = _userProfileService.UploadPhoto(file.InputStream, file.FileName);
				if (!success)
				{
					validation.Add(DictionaryKeys.Messages.UnsuccessfulImageUpload);
				}
			}
			else
			{
				modelState.Errors.ForEach(err => validation.Add(err.ErrorMessage));
			}

			return Json(new { validation, success });
		}
	}
}