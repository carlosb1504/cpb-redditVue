using NUnit.Framework;
using cpb_redditVue.dal.Models;
using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.app.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace cpb_redditVue.tests
{
    public class SubredditControllerTests
    {
        private Mock<ISubredditService> mockSubredditService;
        private Mock<ILogger<SubredditController>> mockLogger;
        private SubredditController controller;

        private readonly string testSubredditName = "funny";
        private readonly Enums.PostsCategory testPostCategory = Enums.PostsCategory.TOP;
        private readonly string testSearchString = "christmas";

        

        [SetUp]
        public void Setup()
        {
            // Arrange
            mockSubredditService = new Mock<ISubredditService>();
            mockSubredditService.Setup(service => service.GetSubredditPosts(this.testSubredditName, this.testPostCategory, ""))
                .Returns(GetTestPostList());            
            mockSubredditService.Setup(service => service.GetSubredditPosts(this.testSubredditName, this.testSearchString, ""))
                .Returns(GetTestPostList());
            mockLogger = new Mock<ILogger<SubredditController>>();

            controller = new SubredditController(mockLogger.Object, mockSubredditService.Object);
        }

        [Test]
        public void SubredditController_GetTopPosts_ReturnsListOfPostDetailObject()
        {
            // Act
            var result = controller.GetSubredditPosts(this.testSubredditName, this.testPostCategory).ToList();

            // Assert
            Assert.IsInstanceOf<IEnumerable<PostDetail>>(result);
            Assert.AreEqual(2, result.ToList().Count);
        }

        [Test]
        public void SubredditController_GetSearchPosts_ReturnsListOfPostDetailObject()
        {
            // Act
            var result = controller.GetSubredditSearchPosts(this.testSubredditName, this.testSearchString).ToList();

            // Assert
            Assert.IsInstanceOf<IEnumerable<PostDetail>>(result);
            Assert.AreEqual(2, result.ToList().Count);
        }

        private IEnumerable<PostDetail> GetTestPostList()
        {
            return new List<PostDetail>
            {
                new PostDetail
                {
                    Title = "Funny post 1"
                },
                new PostDetail
                {
                    Title = "Funny post 2"
                },
            };
        }

    }
}