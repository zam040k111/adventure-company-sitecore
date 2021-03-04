using Adventure.Foundation.SearchProvider.Repositories;
using Adventure.Foundation.SearchProvider.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Foundation.SearchProvider.DependencyConfiguration
{
    public static class RegistrationExtensions
    {
        public static void RegisterSearchRepository<TSearchItem, TResult>(this IServiceCollection services)
	        where TSearchItem : SearchResultItem
	        where TResult : class
        {
	        services.AddScoped<ISearchRepository<TSearchItem, TResult>, SearchRepository<TSearchItem, TResult>>();
        }
    }
}
