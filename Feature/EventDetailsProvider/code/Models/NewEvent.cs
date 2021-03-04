using System;
using System.Runtime.Serialization;

namespace Adventure.Feature.EventDetailsProvider.Models
{
    [DataContract]
    public class NewEvent
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ShortDescription { get; set; }

        [DataMember]
        public int Difficulty { get; set; }

        [DataMember]
        public string ButtonText { get; set; }
    }
}
