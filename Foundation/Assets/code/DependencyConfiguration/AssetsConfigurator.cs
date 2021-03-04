using Adventure.Foundation.Assets.Services;
using Adventure.Foundation.Assets.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Adventure.Foundation.Assets.DependencyConfiguration
{
	public class AssetsConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IRenderAssetsService, RenderAssetsService>();
		}
	}
}