using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.UnitTests.Web.AccountControllerTests
{
    [TestFixture]
    public class LogOutPost
    {
        [Test]
        public void ShouldCallSignOutMethodOfAuthenticationProvider()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedContextProvider = new Mock<IHttpContextProvider>();
            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignOut()).Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var accountController = new AccountController(mockedContextProvider.Object,
                mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object,
                mockedUsersService.Object);

            // Act
            var result = accountController.LogOut();

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.SignOut(), Times.Once);
        }

        [Test]
        public void ShouldCallGetRouteDataMethodOfRouteDataProvider()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedContextProvider = new Mock<IHttpContextProvider>();
            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData)
                .Verifiable();
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignOut());
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var accountController = new AccountController(mockedContextProvider.Object,
                mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object,
                mockedUsersService.Object);

            // Act
            var result = accountController.LogOut();

            // Assert
            mockedRouteDataProvider.Verify(rdp => 
                rdp.GetRouteData(accountController), Times.Once);
        }

        [Test]
        public void ShouldReturnRedirectToRouteResultWithDefaultControllerAndAction()
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedContextProvider = new Mock<IHttpContextProvider>();
            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var accountController = new AccountController(mockedContextProvider.Object,
                mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object,
                mockedUsersService.Object);

            // Act
            var result = accountController.LogOut() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
