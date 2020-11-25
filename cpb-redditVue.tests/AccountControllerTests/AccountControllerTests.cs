using NUnit.Framework;
using cpb_redditVue.dal.Models;
using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.app.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using System;

namespace cpb_redditVue.tests
{
    public class AccountControllerTests
    {
        Mock<IAccountService> accountService;
        Mock<ILogger<AccountController>> mockLogger;
        AccountController controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            accountService = new Mock<IAccountService>();
            accountService.Setup(service => service.GetLoggedInUserDetail())
                .Returns(GetTestAccount());
            mockLogger = new Mock<ILogger<AccountController>>();

            controller = new AccountController(mockLogger.Object, accountService.Object);
        }

        [Test]
        public void AccountController_GetLoggedInAccount_ReturnsAccountDetailObject()
        {
            // Act
            var result = controller.Get();

            // Assert
            Assert.IsInstanceOf<AccountDetail>(result);
            Assert.AreEqual("Hubert Cumberdale", result.FullName);
            Assert.AreEqual("HubertC", result.Username);
        }

        private AccountDetail GetTestAccount()
        {
            return new AccountDetail { FullName = "Hubert Cumberdale", Username = "HubertC" };
        }

    }
}