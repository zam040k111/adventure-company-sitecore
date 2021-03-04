using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Foundation.SearchProvider.Repositories.Interfaces;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace Adventure.Foundation.SearchProvider.Repositories
{
	public class SearchRepository<TSearchItem, TResult> : ISearchRepository<TSearchItem, TResult>
		where TSearchItem : SearchResultItem
		where TResult : class
	{
		private const string StandardValues = "__Standard Values";
		private readonly ISitecoreService _sitecoreService;

		public SearchRepository()
		{
			_sitecoreService = new SitecoreService(Sitecore.Context.Database);
		}

		private IQueryable<TSearchItem> GetAll(
			IQueryable<TSearchItem> queryable,
			Expression<Func<TSearchItem, bool>> filter = null,
			Expression<Func<TSearchItem, object>> orderBy = null)
		{
			if (filter != null && orderBy != null)
			{
				return queryable.Where(filter).OrderBy(orderBy);
			}

			if (filter != null)
			{
				return queryable.Where(filter);
			}

			if (orderBy != null)
			{
				return queryable.OrderBy(orderBy);
			}

			return queryable;
		}

		public IEnumerable<TResult> GetAll(
			ID templateId,
			Expression<Func<TSearchItem, bool>> filter = null,
			Expression<Func<TSearchItem, object>> orderBy = null,
			string indexName = "sitecore_web_index",
			bool showStandardValue = false)
		{
			using (var ctx = ContentSearchManager.GetIndex(indexName).CreateSearchContext())
			{
				var searchResults = GetAll(
						ctx.GetQueryable<TSearchItem>()
							.Where(i => i.TemplateId == templateId && (showStandardValue || i.Name != StandardValues)),
						filter,
						orderBy)
					.GetResults();

				if (searchResults != null && searchResults.TotalSearchResults > 0)
				{
					var data = searchResults.Hits
						.Where(i => i.Document != null)
						.Select(i => i.Document);

					var listResult = new List<TResult>();

					foreach (var searchItem in data)
					{
						listResult.Add(_sitecoreService.GetItem<TResult>(searchItem.GetItem()));
					}

					return listResult;
				}
			}

			return Enumerable.Empty<TResult>();
		}
	}
}