using Adventure.Feature.Events.Services;
using Adventure.Feature.Events.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.Events.ServicesConfigurator
{
	public class ServicesConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IEventsService, EventsService>();
		}
	}
}