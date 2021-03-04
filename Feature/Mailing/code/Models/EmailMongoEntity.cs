using System.Collections.Generic;
using Adventure.Foundation.DataAccess.Entities;

namespace Adventure.Feature.Mailing.Models
{
	public class EmailMongoEntity : MongoBaseEntity
	{
		public string Recipient { get; set; }

		public List<string> EventIds { get; set; }
	}
}