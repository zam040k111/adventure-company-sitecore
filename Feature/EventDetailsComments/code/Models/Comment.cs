using System;
using Adventure.Foundation.DataAccess.Entities;

namespace Adventure.Feature.EventDetailsComments.Models
{
    public class Comment : MongoBaseEntity
    {
        public string AuthorName { get; set; }

        public string Text { get; set; }

        public Guid EventId { get; set; }
    }
}