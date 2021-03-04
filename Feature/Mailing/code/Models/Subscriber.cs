using System.Collections.Generic;
using Adventure.Foundation.DataAccess.Entities;

namespace Adventure.Feature.Mailing.Models
{
	public class Subscriber : MongoBaseEntity
	{
		public string Email { get; set; }

		public bool IsSubscribed { get; set; } = true;
		
		public List<string> Tags { get; set; } = new List<string>();
	}
}