using System.Linq;

namespace Adventure.Feature.EventSearchComponent.Filtering.Interfaces
{
	public interface IFilter<in TFilterModel, TEntity>
    {
		IQueryable<TEntity> Execute(TFilterModel filterModel, IQueryable<TEntity> input);
	}
}
