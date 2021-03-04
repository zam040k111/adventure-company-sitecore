using System.IO;
using Adventure.Feature.UserProfile.Models;

namespace Adventure.Feature.UserProfile.Services.Interfaces
{
	public interface IUserProfileService
	{
		UserProfileModel GetUserProfile();

		bool SetUserProfile(UserProfileModel userProfileModel);

		bool UploadPhoto(Stream fileStream, string fileName);
	}
}
