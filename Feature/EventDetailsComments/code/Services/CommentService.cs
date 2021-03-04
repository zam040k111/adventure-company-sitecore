using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventDetailsComments.Models;
using Adventure.Feature.EventDetailsComments.Services.Interfaces;
using Adventure.Foundation.Common.ValidationServices;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using Adventure.Foundation.DependencyInjection;
using log4net;

namespace Adventure.Feature.EventDetailsComments.Services
{
    [Service(typeof(ICommentService), Lifetime = Lifetime.Scoped)]
    public class CommentService :  ICommentService
    {
        private const string CommentsToDisplayCountMessage = "Comments count to display value must be bigger zero.";
        private readonly IMongoGenericRepository<Comment> _commentRepository;
        private readonly ILog _logger;

        public CommentService(IMongoGenericRepository<Comment> commentRepository, ILog logger)
        {
            Throw.IfNull(commentRepository, nameof(commentRepository));
            Throw.IfNull(logger, nameof(logger));

            _commentRepository = commentRepository;
            _logger = logger;
        }

        public void Add(Comment comment)
        {
            Throw.IfNull(comment, nameof(comment));

            _logger.Debug($"Comment from author {comment.AuthorName} was created in {nameof(Add)}.");

            _commentRepository.Create(comment);
        }

        public IEnumerable<CommentViewModel> Get(int commentsCountToDisplay, Guid eventId)
        {
            _logger.Debug($"{nameof(Get)} has been called.");

            Throw.IfCondition(commentsCountToDisplay < 1, nameof(commentsCountToDisplay), CommentsToDisplayCountMessage);

            var comments = _commentRepository.GetAll(
                filter: comment => comment.EventId == eventId,
                orderBy: comment => comment.DateAdded);
            var commentsActualCount = comments.Count();

            return comments
                .Skip(commentsActualCount < commentsCountToDisplay ? 0 : commentsActualCount - commentsCountToDisplay)
                .Select(x => new CommentViewModel
                {
                    Text = x.Text,
                    AuthorName = x.AuthorName,
                    DateAdded = x.DateAdded
                });
        }
    }
}