using Adventure.Feature.EventSearchComponent.Filtering;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Feature.EventSearchComponent.Repositories;
using Adventure.Feature.EventSearchComponent.Repositories.Interfaces;
using Adventure.Feature.EventSearchComponent.Services;
using Adventure.Feature.EventSearchComponent.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.EventSearchComponent.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventsRepository, EventsRepository>();
            serviceCollection.AddScoped<IEventSearchService, EventSearchService>();
            serviceCollection.AddScoped<IEventPipeline, EventPipeline>();
        }
    }
}