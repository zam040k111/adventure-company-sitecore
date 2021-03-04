using Adventure.Feature.EventDetailsComments.Constants;
using Adventure.Foundation.DataAccess.DependencyConfiguration;
using Adventure.Feature.EventDetailsComments.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.EventDetailsComments.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterMongoRepository<Comment>(Settings.CommentCollectionName);
        }
    }
}