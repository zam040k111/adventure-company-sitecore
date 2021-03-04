using Adventure.Foundation.DataAccess.Constants;
using Adventure.Foundation.DataAccess.Factories;
using Adventure.Foundation.DataAccess.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sitecore.DependencyInjection;

namespace Adventure.Foundation.DataAccess.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMongoClient, MongoClient>();
            RegisterMongoCollectionFactory(serviceCollection);
        }

        private static void RegisterMongoCollectionFactory(IServiceCollection serviceCollection)
        {
            serviceCollection.Add(new ServiceDescriptor(
                typeof(IMongoCollectionFactory),
                x => new MongoCollectionFactory(
                    x.GetService<IMongoClient>(),
                    DatabaseSettings.MongoDatabaseName),
                ServiceLifetime.Scoped));
        }
    }
}