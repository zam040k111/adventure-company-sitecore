using System;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DataAccess.Entities;
using Adventure.Foundation.DataAccess.Factories.Interfaces;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Adventure.Foundation.DataAccess.Repositories
{
    public class MongoGenericRepository<TMongoEntity> : IMongoGenericRepository<TMongoEntity>
        where TMongoEntity : MongoBaseEntity
    {
        private readonly IMongoCollection<TMongoEntity> _collection;

        public MongoGenericRepository(IMongoCollectionFactory factory, string collectionName)
        {
            Throw.IfNull(factory, nameof(factory));
            Throw.IfNull(collectionName, nameof(collectionName));

            _collection = factory.GetCollection<TMongoEntity>(collectionName);
        }

        public void Create(TMongoEntity entity)
        {
            Throw.IfNull(entity, nameof(entity));

            _collection.InsertOne(entity);
        }

        public IQueryable<TMongoEntity> GetAll(
            Expression<Func<TMongoEntity, bool>> filter = null,
            Expression<Func<TMongoEntity, object>> orderBy = null)
        {
	        if (filter != null && orderBy != null)
	        {
		        return _collection.AsQueryable().Where(filter).OrderBy(orderBy);
	        }

	        if (filter != null)
	        {
		        return _collection.AsQueryable().Where(filter);
	        }

	        if (orderBy != null)
	        {
		        return _collection.AsQueryable().OrderBy(orderBy);
	        }

	        return _collection.AsQueryable();
        }

        public TMongoEntity Get(Expression<Func<TMongoEntity, bool>> filter)
        {
            Throw.IfNull(filter, nameof(filter));

            return _collection.AsQueryable().FirstOrDefault(filter);
        }

        public UpdateResult UpdateField<TField>(ObjectId id, Expression<Func<TMongoEntity, TField>> field, TField value)
        {
            Throw.IfCondition(id == ObjectId.Empty, nameof(id),"Id can not be empty");
	        Throw.IfNull(field, nameof(field));
	        Throw.IfNull(value, nameof(value));

	        var update = Builders<TMongoEntity>.Update.Set(field, value);
	        
	        return _collection.UpdateOne(i => i.Id == id, update);
        }

        public TMongoEntity Replace(Expression<Func<TMongoEntity, bool>> filter, TMongoEntity entity)
        {
	        Throw.IfNull(filter, nameof(filter));
	        Throw.IfNull(entity, nameof(entity));

	        return _collection.FindOneAndReplace(filter, entity);
        }
    }
}
