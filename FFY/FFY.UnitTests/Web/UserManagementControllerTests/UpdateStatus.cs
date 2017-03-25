using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.UserManagementControllerTests
{
    [TestFixture]
    public class UpdateStatus
    {
        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel() { UserId = id };
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()));
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(cs => cs.GetUserById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedUsersService.Verify(us => us.GetUserById(id), Times.Once);
        }

        [Test]
        public void ShouldCallChangeUserRoleMethodOfAuthenticationProvider()
        {
            // Arrange
            var id = "123";
            var role = "User";
            var userViewModel = new UserViewModel() { Role = role };
            var user = new User() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(cs => cs.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.ChangeUserRole(id, role), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateSecurityStampOfAuthenticationProvider()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel();
            var user = new User() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(cs => cs.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            userManagementController.UpdateStatus(userViewModel);

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.UpdateSecurityStamp(id), Times.Once);
        }

        [Test]
        public void ShouldRedirectToUserProfileView()
        {
            // Arrange
            var id = "123";
            var userViewModel = new UserViewModel();
            var user = new User() { Id = id };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap =>
                ap.ChangeUserRole(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            mockedAuthenticationProvider.Setup(ap =>
                ap.UpdateSecurityStamp(It.IsAny<string>()));
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(cs => cs.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.UpdateStatus(userViewModel))
                .ShouldRedirectTo((UserManagementController umc) => umc.UserProfile(userViewModel, null, id));
        }
    }
}
