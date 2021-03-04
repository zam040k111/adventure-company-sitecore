using Adventure.Foundation.Assets.Services.Interfaces;
using Feature.Events;

namespace Adventure.Feature.Events.Models
{
	public class CarouselViewModel
	{
		public ICarousel Carousel { get; set; }

		public IRenderAssetsService AssetsService { get; set; }
	}
}