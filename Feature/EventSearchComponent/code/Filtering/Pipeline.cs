using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Sitecore.ContentSearch.SearchTypes;

namespace Adventure.Feature.EventSearchComponent.Filtering
{
    public abstract class Pipeline<TFilterSettings, TEntity> : IPipeline<TFilterSettings, TEntity>
        where TEntity : SearchResultItem
    {
        protected readonly List<IFilter<TFilterSettings, TEntity>> Filters =
            new List<IFilter<TFilterSettings, TEntity>>();

        public int TotalItems { get; set; }

        public IPipeline<TFilterSettings, TEntity> Register(IFilter<TFilterSettings, TEntity> filter)
        {
            Throw.IfNull(filter, nameof(filter));

            Filters.Add(filter);

            return this;
        }

        public abstract IQueryable<TEntity> Process(TFilterSettings filterModel, IQueryable<TEntity> input);
    }
}