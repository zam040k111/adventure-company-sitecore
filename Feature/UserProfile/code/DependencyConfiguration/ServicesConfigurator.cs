using Adventure.Feature.UserProfile.Models;
using Adventure.Feature.UserProfile.Services;
using Adventure.Feature.UserProfile.Services.Interfaces;
using Adventure.Foundation.SearchProvider.DependencyConfiguration;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.UserProfile.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterSearchRepository<EventTagSearchItem, IEventTag>();
	        serviceCollection.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}