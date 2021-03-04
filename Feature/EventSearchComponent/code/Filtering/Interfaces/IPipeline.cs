using System.Linq;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Feature.EventSearchComponent.Filtering.Interfaces
{
    public interface IPipeline<TFilterSettings, TEntity>
        where TEntity : SearchResultItem
    {
        int TotalItems { get; set; }

        IPipeline<TFilterSettings, TEntity> Register(IFilter<TFilterSettings, TEntity> filter);

        IQueryable<TEntity> Process(TFilterSettings filterModel, IQueryable<TEntity> input);
    }
}
