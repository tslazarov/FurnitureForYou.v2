using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.UserManagementControllerTests
{
    [TestFixture]
    public class UserProfile
    {

        [Test]
        public void ShouldSetProfileModelOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "User" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            Assert.AreSame(profileViewModel, userViewModel.ProfileModel);
        }

        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "Administrator" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Verifiable();

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            mockedUsersService.Verify(us => us.GetUserById(id), Times.Once);
        }

        [Test]
        public void ShouldSetUserOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "Administrator" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            Assert.AreSame(user, userViewModel.ProfileModel.User);
        }

        [Test]
        public void ShouldCallGetUserRolesMethodOfAuthenticationProvider()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "Moderator" })
                .Verifiable();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()));

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.GetUserRoles(id), Times.Once);
        }

        [Test]
        public void ShouldSetRoleOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var role = "Administrator";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { role });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            Assert.AreEqual(role, userViewModel.Role);
        }

        [Test]
        public void ShouldSetUserIdOfUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "Moderator" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.UserProfile(userViewModel, profileViewModel, id);

            // Assert
            Assert.AreEqual(id, userViewModel.UserId);
        }

        [Test]
        public void ShouldReturnViewWithUserViewModel()
        {
            // Arrange
            var id = "13";
            var userViewModel = new UserViewModel();
            var profileViewModel = new ProfileViewModel();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.GetUserRoles(It.IsAny<string>()))
                .Returns(new List<string>() { "User" });
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var usersManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act
            usersManagementController.WithCallTo(umc =>
                umc.UserProfile(userViewModel, profileViewModel, id))
                .ShouldRenderDefaultView()
                .WithModel<UserViewModel>(model => Assert.AreEqual(userViewModel, model));
        }
    }
}
