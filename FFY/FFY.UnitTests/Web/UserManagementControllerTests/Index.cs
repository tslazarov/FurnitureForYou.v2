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
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultViewWithUsersViewModel()
        {
            // Arrange
            var usersViewModel = new UsersViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.Index(usersViewModel))
                .ShouldRenderDefaultView()
                .WithModel<UsersViewModel>(model => Assert.AreEqual(usersViewModel, model));
        }
    }
}
