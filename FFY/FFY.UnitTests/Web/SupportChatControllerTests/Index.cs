using FFY.Models;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.SupportChat;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.SupportChatControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldCallGetChatUsersMethodOfChatUsersService()
        {
            // Arrange
            var supportChatViewModel = new SupportChatViewModel();

            var mockedChatUsersService = new Mock<IChatUsersService>();
            mockedChatUsersService.Setup(cus => cus.GetChatUsers()).Verifiable();

            var supportChatManagementController = new SupportChatController(mockedChatUsersService.Object);

            // Act
            supportChatManagementController.Index(supportChatViewModel);

            // Assert
            mockedChatUsersService.Verify(cus => cus.GetChatUsers(), Times.Once);
        }

        [Test]
        public void ShouldSetConnectedUsersOfSupportChatViewModel()
        {
            // Arrange
            var supportChatViewModel = new SupportChatViewModel();

            var chatUsers = new List<ChatUser>();

            var mockedChatUsersService = new Mock<IChatUsersService>();
            mockedChatUsersService.Setup(cus => cus.GetChatUsers())
                .Returns(chatUsers);

            var supportChatManagementController = new SupportChatController(mockedChatUsersService.Object);

            // Act
            supportChatManagementController.Index(supportChatViewModel);

            // Assert
            CollectionAssert.AreEquivalent(chatUsers, supportChatViewModel.ConnectedUsers);
        }

        [Test]
        public void ShouldReturnDefaultViewWithSupportChatViewModel()
        {
            // Arrange
            var supportChatViewModel = new SupportChatViewModel();

            var mockedChatUsersService = new Mock<IChatUsersService>();

            var supportChatManagementController = new SupportChatController(mockedChatUsersService.Object);

            // Act and Assert
            supportChatManagementController.WithCallTo(scmc => scmc.Index(supportChatViewModel))
                .ShouldRenderDefaultView()
                .WithModel<SupportChatViewModel>(model => Assert.AreEqual(supportChatViewModel, model));
        }
    }
}
