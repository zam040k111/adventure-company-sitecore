using System;
using System.Collections.Generic;
using Feature.EventDetailsComments;

namespace Adventure.Feature.EventDetailsComments.Models
{
    public class CommentSectionViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public Guid EventId { get; set; }

        public IComment CommentSettings { get; set; }
    }
}
