namespace Adventure.Foundation.DependencyInjection.Infrastructure
{
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddClassesWithServiceAttribute("*.Foundation.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Feature.*");
            serviceCollection.AddMvcControllers("*.Feature.*");
            serviceCollection.AddTransient<IMvcContext, MvcContext>();
            serviceCollection.Add(new ServiceDescriptor(
                typeof(ISitecoreService),
                x => new SitecoreService(Sitecore.Context.Database),
                ServiceLifetime.Scoped));
        }
    }
}