using Adventure.Foundation.DataAccess.DependencyConfiguration;
using Adventure.Feature.Mailing.Constants;
using Adventure.Feature.Mailing.Models;
using Adventure.Feature.Mailing.Services;
using Adventure.Feature.Mailing.Services.Interfaces;
using Adventure.Foundation.SearchProvider.DependencyConfiguration;
using Foundation.ORM.Sitecore.templates.Project.Adventure;
using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.Mailing.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
	        if (Glass.Mapper.Context.Default == null)
	        {
		        var config = new Config();
		        var dependencyResolver = new Glass.Mapper.Sc.IoC.DependencyResolver(config);
		        Glass.Mapper.Context.Create(dependencyResolver, DataAccess.GlassContextName, true);
		        
		        serviceCollection.Add(new ServiceDescriptor(
			        typeof(ISitecoreService),
			        x => new SitecoreService(Database.GetDatabase(DataAccess.SitecoreDbName)),
			        ServiceLifetime.Scoped));
            }

            serviceCollection.RegisterMongoRepository<Subscriber>(DataAccess.SubscribersCollectionName);
            serviceCollection.RegisterMongoRepository<EmailMongoEntity>(DataAccess.SentEmailsCollectionName);
            serviceCollection.RegisterSearchRepository<EventTagSearchItem, IEventTag>();
            serviceCollection.RegisterSearchRepository<EventDetailsPageSearchItem, IEventDetailsPage>();
            serviceCollection.AddScoped<IEmailGenerator, EmailGenerator>();
            serviceCollection.AddScoped<IEventSenderService, EventSenderService>();
            serviceCollection.AddScoped<IMailingService, MailingService>();
        }
    }
}