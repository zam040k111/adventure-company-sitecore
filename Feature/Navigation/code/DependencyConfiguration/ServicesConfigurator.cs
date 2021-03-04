using Adventure.Feature.Navigation.Repositories;
using Adventure.Feature.Navigation.Repositories.Interfaces;
using Adventure.Feature.Navigation.Services;
using Adventure.Feature.Navigation.Services.Interfaces;
using Adventure.Feature.Navigation.Utilities;
using Adventure.Feature.Navigation.Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.Navigation.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INavigationService, NavigationService>();
            serviceCollection.AddScoped<IEventListRepository, EventListRepository>();
            serviceCollection.AddScoped<ISearchSettingsReader, SearchSettingsReader>();
        }
    }
}