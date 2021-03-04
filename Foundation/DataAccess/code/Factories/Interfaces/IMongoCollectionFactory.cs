using MongoDB.Driver;

namespace Adventure.Foundation.DataAccess.Factories.Interfaces
{
    public interface IMongoCollectionFactory
    {
        IMongoCollection<TMongoEntity> GetCollection<TMongoEntity>(string collectionName);
    }
}