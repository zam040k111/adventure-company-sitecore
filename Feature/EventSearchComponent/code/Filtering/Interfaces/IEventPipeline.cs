using Adventure.Feature.EventSearchComponent.Models;

namespace Adventure.Feature.EventSearchComponent.Filtering.Interfaces
{
    public interface IEventPipeline : IPipeline<EventSearchSettings, EventDetailsSearchItem>
    {
    }
}
