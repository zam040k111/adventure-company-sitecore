using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace Adventure.Foundation.SearchProvider.Repositories.Interfaces
{
	public interface ISearchRepository<TSearchItem, out TResult>
		where TSearchItem : SearchResultItem
		where TResult : class
	{
		IEnumerable<TResult> GetAll(
			ID templateId,
			Expression<Func<TSearchItem, bool>> filter = null,
			Expression<Func<TSearchItem, object>> orderBy = null,
			string indexName = "sitecore_web_index",
			bool showStandardValue = false);
	}
}
