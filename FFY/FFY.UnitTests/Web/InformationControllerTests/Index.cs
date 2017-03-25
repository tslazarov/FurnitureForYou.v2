using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.InformationControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "424";
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap =>ap.CurrentUserId)
                .Returns(id);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>())).Verifiable();

            var informationController = new InformationController(mockedAuthenticationProvider.Object,
                mockedUsersService.Object);

            // Act
            informationController.Index(profileViewModel);

            // Assert
            mockedUsersService.Verify(us => us.GetUserById(id), Times.Once);
        }

        [Test]
        public void ShouldGetCurrentUserIdOfAuthenticationProvider()
        {
            // Arrange
            var id = "424";
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id)
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()));

            var informationController = new InformationController(mockedAuthenticationProvider.Object,
                mockedUsersService.Object);

            // Act
            informationController.Index(profileViewModel);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldSetUserOfProfileViewModel()
        {
            // Arrange
            var id = "424";
            var user = new User();
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var informationController = new InformationController(mockedAuthenticationProvider.Object,
                mockedUsersService.Object);

            // Act
            informationController.Index(profileViewModel);

            // Assert
            Assert.AreSame(user, profileViewModel.User);
        }

        [Test]
        public void ShouldReturnDefaultViewWithProfileViewModel()
        {
            var id = "424";
            var user = new User();
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var informationController = new InformationController(mockedAuthenticationProvider.Object,
                mockedUsersService.Object);

            // Act and Assert
            informationController.WithCallTo(ic => ic.Index(profileViewModel))
                .ShouldRenderDefaultView()
                .WithModel<ProfileViewModel>(model => Assert.AreEqual(profileViewModel, model));
        }
    }
}
