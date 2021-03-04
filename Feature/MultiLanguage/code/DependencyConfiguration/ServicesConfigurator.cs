using Adventure.Feature.MultiLanguage.Services;
using Adventure.Feature.MultiLanguage.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Feature.MultiLanguage.DependencyConfiguration
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMultiLanguageService, MultiLanguageService>();
        }
    }
}