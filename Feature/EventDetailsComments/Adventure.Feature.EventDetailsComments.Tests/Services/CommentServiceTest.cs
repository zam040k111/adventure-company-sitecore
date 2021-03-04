using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Feature.EventDetailsComments.Models;
using Adventure.Feature.EventDetailsComments.Services;
using Adventure.Foundation.DataAccess.Repositories.Interfaces;
using log4net;
using Moq;
using NUnit.Framework;

namespace Adventure.Feature.EventDetailsComments.Tests.Services
{
    [TestFixture]
    public class CommentServiceTest
    {
        private CommentService _commentService;
        private Mock<IMongoGenericRepository<Comment>> _commentRepository;

        [SetUp]
        public void SetUp()
        {
            _commentRepository = new Mock<IMongoGenericRepository<Comment>>();

            _commentService = new CommentService(_commentRepository.Object, Mock.Of<ILog>());
        }

        [Test]
        public void Add_InputNull_ShouldNotCallCreateMethod()
        {
            Assert.Throws<ArgumentNullException>(() => _commentService.Add(null));

            _commentRepository.Verify(x => x.Create(It.IsAny<Comment>()), Times.Never);
        }

        [Test]
        public void Add_InputNotNull_ShouldCallCreateMethodWithProperParameter()
        {
            var comment = new Comment
            {
                AuthorName = "name",
                Text = "text"
            };

            _commentService.Add(comment);

            _commentRepository.Verify(
                x => x.Create(
                    It.Is<Comment>(
                        y => y.AuthorName == comment.AuthorName && y.Text == comment.Text)),
                Times.Once);
        }

        [Test]
        public void Get_ShouldReturnProperResult()
        {
            var displayCount = 2;
            var comments = new List<Comment>
            {
                new Comment { AuthorName = "name1", Text = "text1" },
                new Comment { AuthorName = "name2", Text = "text2" },
                new Comment { AuthorName = "name3", Text = "text3" }
            };
            _commentRepository
                .Setup(
                    x => x.GetAll(
                        It.IsAny<Expression<Func<Comment, bool>>>(),
                        It.IsAny<Expression<Func<Comment, object>>>()))
                .Returns(comments.AsQueryable());

            var result = _commentService.Get(displayCount, Guid.NewGuid());

            Assert.IsNotNull(result);
            Assert.AreEqual(displayCount, result.Count());
            Assert.IsTrue(
                comments[1].AuthorName == result.ElementAt(0).AuthorName
                && comments[1].Text == result.ElementAt(0).Text
                && comments[2].AuthorName == result.ElementAt(1).AuthorName
                && comments[2].Text == result.ElementAt(1).Text);
        }
    }
}