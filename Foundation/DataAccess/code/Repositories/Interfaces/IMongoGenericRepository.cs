using System;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Foundation.DataAccess.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Adventure.Foundation.DataAccess.Repositories.Interfaces
{
    public interface IMongoGenericRepository<TMongoEntity>
     where TMongoEntity : MongoBaseEntity
    {
        void Create(TMongoEntity entity);

        TMongoEntity Get(Expression<Func<TMongoEntity, bool>> filter);

        IQueryable<TMongoEntity> GetAll(
            Expression<Func<TMongoEntity, bool>> filter = null,
            Expression<Func<TMongoEntity, object>> orderBy = null);

        UpdateResult UpdateField<TField>(ObjectId id, Expression<Func<TMongoEntity, TField>> field, TField value);

        TMongoEntity Replace(Expression<Func<TMongoEntity, bool>> filter, TMongoEntity entity);
    }
}
