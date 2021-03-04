using Adventure.Foundation.Assets.Helpers;

namespace Adventure.Foundation.Assets.Services
{
    using Interfaces;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Models;
    using Repositories;

    public class RenderAssetsService : IRenderAssetsService
    {
        public HtmlString RenderScript()
        {
            var assets = AssetRepository.Current.Items
	            .Where(asset => (asset.Type == AssetType.JavaScript || asset.Type == AssetType.Raw));

            var sb = new StringBuilder();
            foreach (var item in assets)
            {
                if (item.Type == AssetType.Raw)
                {
                    sb.Append(item.Content).AppendLine();
                }
                else
                {
	                sb.AppendFormat(TagHelper.Script(item.Content)).AppendLine();
                }
            }
            return new HtmlString(sb.ToString());
        }

        public HtmlString RenderStyles()
        {
            var sb = new StringBuilder();
            foreach (var item in AssetRepository.Current.Items
	            .Where(asset => asset.Type == AssetType.Css))
            {
	            sb.AppendFormat(TagHelper.Style(item.Content)).AppendLine();
            }

            return new HtmlString(sb.ToString());
        }
    }
}