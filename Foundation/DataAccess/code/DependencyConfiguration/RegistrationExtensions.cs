using Adventure.Foundation.DataAccess.Entities;
using Adventure.Foundation.DataAccess.Factories.Interfaces;
using Adventure.Foundation.DataAccess.Repositories;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Adventure.Foundation.DataAccess.DependencyConfiguration
{
    public static class RegistrationExtensions
    {
        public static void RegisterMongoRepository<TMongoEntity>(this IServiceCollection services, string collectionName)
            where TMongoEntity : MongoBaseEntity
        {
            services.Add(new ServiceDescriptor(
                typeof(IMongoGenericRepository<TMongoEntity>),
                x => new MongoGenericRepository<TMongoEntity>(
                    x.GetService<IMongoCollectionFactory>(),
                    collectionName),
                ServiceLifetime.Scoped));
        }
    }
}
