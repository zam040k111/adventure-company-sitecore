using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Web;
using System.IO;
using Adventure.Feature.ErrorHandling.Constants;
using Adventure.Foundation.Common.ValidationServices;
using Sitecore.Data;

namespace Adventure.Feature.ErrorHandling.Pipelines.httpRequestBegin
{
	public class NotFoundProcessor : HttpRequestProcessor
	{
		protected readonly string Path = "/sitecore";
		protected readonly string AjaxRequest = "/api";

		protected virtual Item NotFound => Context.Database != null ? Context.Database.GetItem(new ID(NotFoundPage.Id)) : null;

		public override void Process(HttpRequestArgs args)
		{
			Throw.IfNull(args, nameof(args));

			if (NotFound == null || !ShouldProcess(args))
			{
				return;
			}

			Context.Item = NotFound;
		}

		public bool ShouldProcess(HttpRequestArgs args)
		{
			return !IsValidItem
			       || args.LocalPath.StartsWith(Path)
			       || args.Url.FilePath.StartsWith(Path)
			       || File.Exists(System.Web.HttpContext.Current.Server.MapPath(args.Url.FilePath))
			       || Context.Site == null
			       || args.PermissionDenied;
		}

		protected virtual bool IsValidItem
		{
			get
			{
				if (Context.Request.ItemPath != null && Context.Request.ItemPath.Contains(AjaxRequest))
				{
					return true;
				}

				if (Context.Item == null)
				{
					return false;
				}

				return (Context.Item.Visualization.Layout != null)
				       || !string.IsNullOrEmpty(WebUtil.GetQueryString("sc_layout"));
			}
		}
	}
}