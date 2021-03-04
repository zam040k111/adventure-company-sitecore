using System.Collections.Generic;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.UserProfile.Models
{
	public class UserProfileModel
	{
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		public string Photo { get; set; }
		
		public string Phone { get; set; }
		
		public string Title { get; set; }
		
		public string Email { get; set; }

		public string OldPassword { get; set; }
		
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public List<IEventTag> Tags { get; set; }

		public List<string> SelectedTags { get; set; }

		public List<IEventTag> UnselectedTags { get; set; }
	}
}