using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adventure.Feature.UserProfile.Constants;
using Adventure.Feature.UserProfile.Models;
using Adventure.Feature.UserProfile.Services.Interfaces;
using Adventure.Foundation.Common.Helpers;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.SearchProvider.Repositories.Interfaces;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;

namespace Adventure.Feature.UserProfile.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly string _defaultPhoto = TranslateHelper.TranslateMessage(DictionaryKeys.DefaultPhoto.Path);
		private readonly ISearchRepository<EventTagSearchItem, IEventTag> _tagRepository;
		private const string LastName = "LastName";
		private const string Phone = "Phone";
		private const string Title = "Title";
		private const string Tags = "Tags";

		public UserProfileService(ISearchRepository<EventTagSearchItem, IEventTag> tagRepository)
		{
			Throw.IfNull(tagRepository, nameof(tagRepository));

			_tagRepository = tagRepository;
		}

		public UserProfileModel GetUserProfile()
		{
			var userProfile = Context.User.Profile;
			if (string.IsNullOrWhiteSpace(userProfile.Icon))
			{
				var imageItem = Context.Database.GetItem(_defaultPhoto);
				userProfile.Icon = MediaManager.GetMediaUrl(imageItem);
			}

			var tags = GetTagList(userProfile.GetCustomProperty(Tags));
			var unselectedTags = GetUnselectedList(tags);

			return new UserProfileModel
			{
				Email = userProfile.Email,
				FirstName = userProfile.Name,
				LastName = userProfile.GetCustomProperty(LastName),
				Phone = userProfile.GetCustomProperty(Phone),
				Photo = userProfile.Icon,
				Title = userProfile.GetCustomProperty(Title),
				Tags = tags,
				UnselectedTags = unselectedTags
			};
		}

		private List<IEventTag> GetUnselectedList(List<IEventTag> selectedTags)
		{
			var allTags = _tagRepository.GetAll(EventTag.TemplateId);

			return allTags.Where(i => selectedTags.All(tag => tag.TagName != i.TagName)).ToList();
		}

		private List<IEventTag> GetTagList(string propertyValue)
		{
			if (string.IsNullOrWhiteSpace(propertyValue))
			{
				return new List<IEventTag>();
			}

			var tags = JsonConvert.DeserializeObject<List<string>>(propertyValue);

			return _tagRepository
				.GetAll(EventTag.TemplateId, tag => tags.Contains(tag.TagName))
				.ToList();
		}

		private string TagListToString(List<string> tags)
		{
			if (tags == null || !tags.Any())
			{
				return "";
			}

			return JsonConvert.SerializeObject(tags);
		}

		public bool SetUserProfile(UserProfileModel userProfileModel)
		{
			var result = true;
			if (string.IsNullOrWhiteSpace(userProfileModel.OldPassword))
			{
				return false;
			}

			var username = Context.User.Profile.UserName;
			if (System.Web.Security.Membership.ValidateUser(username, userProfileModel.OldPassword))
			{
				Context.User.Profile.Email = userProfileModel.Email;
				Context.User.Profile.Name = userProfileModel.FirstName;
				Context.User.Profile.SetCustomProperty(LastName, userProfileModel.LastName ?? "");
				Context.User.Profile.SetCustomProperty(Phone, userProfileModel.Phone ?? "");
				Context.User.Profile.SetCustomProperty(Title, userProfileModel.Title ?? "");
				Context.User.Profile.SetCustomProperty(Tags, TagListToString(userProfileModel.SelectedTags));

				if (!string.IsNullOrWhiteSpace(userProfileModel.Password)
				    && userProfileModel.Password.Equals(userProfileModel.ConfirmPassword))
				{
					var user = System.Web.Security.Membership.GetUser();
					result = user != null && user.ChangePassword(userProfileModel.OldPassword, userProfileModel.Password);
				}

				Context.User.Profile.Save();
			}
			else
			{
				return false;
			}

			return result;
		}

		public bool UploadPhoto(Stream fileStream, string fileName)
		{
			try
			{
				var path = TranslateHelper.TranslateMessage(DictionaryKeys.PhotoSaveTo.Path) + fileName.Split('.')[0];
				using (var memoryStream = new MemoryStream())
				{
					fileStream.CopyTo(memoryStream);
					var mediaCreator = new MediaCreator();
					var options = new MediaCreatorOptions
					{
						Versioned = false,
						IncludeExtensionInItemName = false,
						Database = Context.Database,
						Destination = path
					};

					using (new SecurityDisabler())
					{
						var item = mediaCreator.CreateFromStream(memoryStream, fileName, options);
						var userProfile = Context.User.Profile;
						if (userProfile != null)
						{
							userProfile.Icon = MediaManager.GetMediaUrl(item);
							Context.User.Profile.Save();
						}
					}
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}