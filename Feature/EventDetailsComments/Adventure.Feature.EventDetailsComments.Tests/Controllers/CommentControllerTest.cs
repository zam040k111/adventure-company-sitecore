using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Adventure.Feature.EventDetailsComments.Controllers;
using Adventure.Feature.EventDetailsComments.Models;
using Adventure.Feature.EventDetailsComments.Services.Interfaces;
using Glass.Mapper.Sc.Web.Mvc;
using log4net;
using Moq;
using NUnit.Framework;
using Sitecore.Data.Items;

namespace Adventure.Feature.EventDetailsComments.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTest
    {
        private CommentController _commentController;
        private Mock<ICommentService> _commentService;
        private Mock<IMvcContext> _mvcContext;

        [SetUp]
        public void SetUp()
        {
            _commentService = new Mock<ICommentService>();
            _commentService
                .Setup(x => x.Get(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns(GetCommentList.AsQueryable());

            _mvcContext = new Mock<IMvcContext>();
            _mvcContext
                .Setup(x => x.DataSourceItem)
                .Returns((Item)null);

            _commentController = new CommentController(
                _commentService.Object,
                Mock.Of<ILog>(),
                _mvcContext.Object);
        }

        [Test]
        public void CommentSection_ShouldCallNeededMethodReturnProperView()
        {
            var expectedCommentsCount = 5;
            var expectedEventId = Guid.Empty;

            Assert.Throws<ArgumentNullException>(() => _commentController.CommentSection());

            _commentService.Verify(x => x.Get(expectedCommentsCount, expectedEventId), Times.Never);
        }

        [Test]
        public void Add_ModelValid_ShouldReturnNotNullResultAndCallMethod()
        {
            var result = _commentController.Add(GetAddCommentViewModel, new Validators.AddCommentValidator());

            Assert.IsNotNull(result);
            _commentService.Verify(x => x.Add(It.Is<Comment>(
                y => y.AuthorName == GetAddCommentViewModel.AuthorName
                     && y.Text == GetAddCommentViewModel.Text)), Times.Once);
        }

        [Test]
        public void Add_ModelInvalid_ShouldReturnNotNullResultAndDoNotCallMethod()
        {
            var result = _commentController.Add(new AddCommentViewModel(), new Validators.AddCommentValidator());

            Assert.IsNotNull(result);
            _commentService.Verify(x => x.Add(It.Is<Comment>(
                y => y.AuthorName == GetAddCommentViewModel.AuthorName
                     && y.Text == GetAddCommentViewModel.Text)), Times.Never);
        }

        [Test]
        public void GetDisplayed_ShouldCallMethodsWithProperParameters()
        {
            var evenId = Guid.NewGuid();
            var commentsCount = 3;

            var result = _commentController.GetDisplayed(evenId, commentsCount);

            Assert.IsNotNull(result);
            _commentService.Verify(x => x.Get(commentsCount, evenId));
        }

        private List<CommentViewModel> GetCommentList { get; } = new List<CommentViewModel>
            { new CommentViewModel(), new CommentViewModel() };

        private AddCommentViewModel GetAddCommentViewModel { get; } = new AddCommentViewModel
            { AuthorName = "name", Text = "text111111111", EventId = Guid.NewGuid() };

    
    }
}


