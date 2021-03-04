using System.Web;

namespace Adventure.Foundation.Assets.Services.Interfaces
{
	public interface IRenderAssetsService
	{
		/// <summary>
		/// Render script tag where src= file path from asset template 
		/// </summary>
		/// <returns>Script including</returns>
		HtmlString RenderScript();
		/// <summary>
		/// Render link tag where href= file path from asset template 
		/// </summary>
		/// <returns>Style including</returns>
		HtmlString RenderStyles();
	}
}
