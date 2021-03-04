using System;

namespace Adventure.Feature.EventDetailsProvider.Models
{
    public class EventProviderSettings : ICloneable
    {
        public string SearchIndex { get; set; }

        public string RequestUrl { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
