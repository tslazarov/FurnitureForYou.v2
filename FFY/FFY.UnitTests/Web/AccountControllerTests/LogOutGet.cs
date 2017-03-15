using FFY.Data.Factories;
using FFY.IdentityConfig.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FFY.UnitTests.Web.AccountControllerTests
{
    [TestFixture]
    public class LogOutGet
    {
        [Test]
        public void ShouldCallSignOutMethodOfAuthenticationProvider()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignOut()).Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.LogOut();

            // Assert
            mockedAuthenticationProvider.Verify(ap => ap.SignOut(), Times.Once);
        }

        [Test]
        public void ShouldReturnRedirectToRouteResultWithDefaultControllerAndAction()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.LogOut() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
