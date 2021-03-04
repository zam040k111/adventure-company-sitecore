using System;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Feature.EventDetailsComments.Models
{
    public class AddCommentViewModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Name")]
        public string AuthorName { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Comment text")]
        public string Text { get; set; }

        public Guid? EventId { get; set; }
    }
}