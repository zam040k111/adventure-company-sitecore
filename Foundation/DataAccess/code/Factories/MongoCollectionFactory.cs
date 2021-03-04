using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DataAccess.Factories.Interfaces;
using MongoDB.Driver;
using Sitecore.Diagnostics;

namespace Adventure.Foundation.DataAccess.Factories
{
    public class MongoCollectionFactory : IMongoCollectionFactory
    {
        private readonly IMongoDatabase _database;

        public MongoCollectionFactory(IMongoClient client, string databaseName)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(databaseName, nameof(databaseName));

            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TMongoEntity> GetCollection<TMongoEntity>(string collectionName)
        {
            Throw.IfNull(collectionName, nameof(collectionName));

            return _database.GetCollection<TMongoEntity>(collectionName);
        }
    }
}