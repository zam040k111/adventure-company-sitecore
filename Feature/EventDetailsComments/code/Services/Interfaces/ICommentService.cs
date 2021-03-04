using System;
using System.Collections.Generic;
using Adventure.Feature.EventDetailsComments.Enums;
using Adventure.Feature.EventDetailsComments.Models;
using Sitecore.Data.Items;

namespace Adventure.Feature.EventDetailsComments.Services.Interfaces
{
    public interface ICommentService
    {
        void Add(Comment comment);

        IEnumerable<CommentViewModel> Get(int commentsCountToDisplay, Guid eventId);
    }
}
