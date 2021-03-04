using System;

namespace Adventure.Feature.Navigation.Models
{
    public class SearchSettings : ICloneable
    {
        public string IndexName { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
