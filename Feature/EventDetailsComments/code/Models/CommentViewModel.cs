using System;

namespace Adventure.Feature.EventDetailsComments.Models
{
    public class CommentViewModel
    {
        public string AuthorName { get; set; }

        public string Text { get; set; }

        public DateTime DateAdded { get; set; }
    }
}