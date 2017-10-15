﻿namespace CommonNews.Web.Tests.Controllers.Tests
{
    using AutoMapper;
    using Data.Models;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using Web.Controllers;
    using Web.ViewModels.Comment;

    [TestFixture]
    public class CommentsControllerTests
    {
        [Test]
        public void Constructor_ShouldReturnObject()
        {
            // Arrange
            var postsServiceMock = new Mock<IDataService<Post>>();
            var commentsServiceMock = new Mock<IDataService<Comment>>();
            var usersServiceMock = new Mock<IDataService<ApplicationUser>>();
            var mockedMapper = new Mock<IMapper>();

            // Act
            var controller = new CommentsController(
                postsServiceMock.Object,
                commentsServiceMock.Object,
                usersServiceMock.Object,
                mockedMapper.Object);

            // Assert
            Assert.IsInstanceOf<CommentsController>(controller);
        }

        //[Test]
        //public void CreateShould_ShouldRedirectToMainPage()
        //{
        //    // Arrange
        //    var commentViewModelMock = new Mock<CommentViewModel>();
        //    commentViewModelMock.Object.Id = -100;
        //    var postsServiceMock = new Mock<IDataService<Post>>();
        //    var commentsServiceMock = new Mock<IDataService<Comment>>();
        //    var usersServiceMock = new Mock<IDataService<ApplicationUser>>();
        //    var mockedMapper = new Mock<IMapper>();

        //    var controller = new CommentsController(
        //        postsServiceMock.Object,
        //        commentsServiceMock.Object,
        //        usersServiceMock.Object,
        //        mockedMapper.Object);

        //    // Act & Assert
        //    controller.WithCallTo(c => c.Create(1, commentViewModelMock.Object))
        //        .ShouldRedirectTo<HomeController>(typeof(PostsController).GetMethod("Details"));
        //}
    }
}
