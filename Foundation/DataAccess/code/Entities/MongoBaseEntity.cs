using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adventure.Foundation.DataAccess.Entities
{
    public abstract class MongoBaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}