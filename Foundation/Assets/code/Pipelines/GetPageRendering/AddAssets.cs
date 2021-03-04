using System;
using System.Linq;
using Adventure.Foundation.Assets.Models;
using Adventure.Foundation.Assets.Repositories;
using Foundation.Assets.Constants;
using Glass.Mapper;
using Sitecore.Data.Items;

namespace Adventure.Foundation.Assets.Pipelines.GetPageRendering
{
    using Sitecore.Mvc.Pipelines.Response.GetPageRendering;

    public class AddAssets : GetPageRenderingProcessor
    {
        public void AddAsset(Item item)
        {
	        if (item == null)
	        {
		        return;
	        }

	        var scriptFiles = item[RenderingAssets.Fields.ScriptFiles];
	        var stylingFiles = item[RenderingAssets.Fields.StylingFiles];
	        var inlineScript = item[RenderingAssets.Fields.InlineScript];
	        var inlineStyling = item[RenderingAssets.Fields.InlineStyling];
	        var useInlineScript = string.Equals(item[RenderingAssets.Fields.UseInlineScript], "1");
	        var useInlineStyling = string.Equals(item[RenderingAssets.Fields.UseInlineStyling], "1");

	        TrySplitFileNames(AssetRepository.Current.AddScriptFile, useInlineScript ? inlineScript : scriptFiles);
	        TrySplitFileNames(AssetRepository.Current.AddStylingFile, useInlineStyling ? inlineStyling : stylingFiles);
        }

        private void TrySplitFileNames(Func<string,Asset> addFileFunc, string fileName)
        {
	        var files = fileName.Split(',');
	        files.Where(i => i.Length > 3).ForEach(i => addFileFunc.Invoke(i));
        }

        public override void Process(GetPageRenderingArgs args)
        {
	        var item = args.PageContext.Item;
			var ancestor = item.DescendsFrom(RenderingAssets.TemplateId) 
				? item 
				: item.Axes.GetAncestors().LastOrDefault(i => i.DescendsFrom(RenderingAssets.TemplateId));

			AddAsset(ancestor);
        }
    }
}