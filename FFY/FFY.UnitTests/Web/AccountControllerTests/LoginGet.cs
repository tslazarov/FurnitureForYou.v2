﻿using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.AccountControllerTests
{
    [TestFixture]
    public class LoginGet
    {
        [Test]
        public void ShouldSetReturnUrlToAccountControllersViewBag()
        {
            // Arrange
            var returnUrl = "/Home/Index";
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var accountController = new AccountController(mockedCachingProvider.Object,
                mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object,
                mockedUsersService.Object);

            // Act
            var result = accountController.Login(returnUrl);

            // Assert
            Assert.AreEqual(returnUrl, accountController.ViewBag.ReturnUrl);
        }

        [Test]
        public void ShouldReturnViewResult()
        {
            // Arrange
            var returnUrl = "/Home/Index";
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var accountController = new AccountController(mockedCachingProvider.Object,
                mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object,
                mockedUsersService.Object);

            // Act and Assert
            accountController.WithCallTo(ac => ac.Login(returnUrl))
                .ShouldRenderDefaultView();
        }
    }
}
