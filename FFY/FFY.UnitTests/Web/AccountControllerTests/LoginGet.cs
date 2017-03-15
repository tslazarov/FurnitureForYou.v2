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
    public class LoginGet
    {
        [Test]
        public void ShouldSetReturnUrlToAccountControllersViewBag()
        {
            // Arrange
            var returnUrl = "/Home/Index";
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

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
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Login(returnUrl);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
