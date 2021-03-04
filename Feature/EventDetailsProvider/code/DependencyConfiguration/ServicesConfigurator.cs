using Adventure.Feature.EventDetailsProvider.Repositories;
using Adventure.Feature.EventDetailsProvider.Repositories.Interfaces;
using Adventure.Feature.EventDetailsProvider.Services;
using Adventure.Feature.EventDetailsProvider.Services.Interfaces;
using Adventure.Feature.EventDetailsProvider.Utilities;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.EventDetailsProvider.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDatabaseWorkerService, DatabaseWorkerService>();
            serviceCollection.AddScoped<IEventCreatorService, EventCreatorService>();
            serviceCollection.AddScoped<IResponseParserService, ResponseParserService>();
            serviceCollection.AddScoped<IResponseDataStreamReaderService, ResponseDataStreamReaderService>();
            serviceCollection.AddScoped<IProviderCallerService, ProviderCallerService>();
            serviceCollection.AddScoped<IRandomEventService, RandomEventService>();
            serviceCollection.AddScoped<ISerializingService, SerializingService>();
            serviceCollection.AddScoped<IEventListRepository, EventListRepository>();
            serviceCollection.AddScoped<IEventProviderSettingsReader, EventProviderSettingsReader>();
            serviceCollection.AddScoped<IEventSourceReader, EventSourceReader>();
            serviceCollection.AddScoped<IEventDataProvider, EventDataProvider>();
        }
    }
}