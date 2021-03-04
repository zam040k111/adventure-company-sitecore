using Feature.Events;
using Feature.Events.Footer;
using Foundation.ORM.Sitecore.templates.Project.Adventure;

namespace Adventure.Feature.Events.Services.Interfaces
{
	public interface IEventsService
	{
		/// <summary>
		/// Returns Carousel element if it is available and contains items, otherwise null.
		/// </summary>
		/// <returns></returns>
		ICarousel GetCarousel();

		/// <summary>
		/// Returns UpcomingEvents element if it is available, otherwise null.
		/// </summary>
		/// <returns></returns>
		IUpcomingEvents GetUpcomingEvents();

		/// <summary>
		/// Returns RichEvent element if it is available, otherwise null.
		/// </summary>
		/// <returns></returns>
		IEventDetailsPage GetRichEvent();

		/// <summary>
		/// Returns FeaturedEvent element if it is available, otherwise null.
		/// </summary>
		/// <returns></returns>
		IEventDetailsPage GetFeaturedEvent();

		/// <summary>
		/// Returns Footer element if it is available, otherwise null.
		/// </summary>
		/// <returns></returns>
		IFooter GetFooter();

		/// Returns Google Map element if it is available.
		/// </summary>
		/// <returns></returns>
		IGoogleMap GetMap();
	}
}