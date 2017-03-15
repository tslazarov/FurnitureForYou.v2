using FFY.Data.Factories;
using FFY.IdentityConfig.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class RegisterPost
    {
        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldReturnViewResultWithModel_WhenModelStateOfControllerIsNotValid(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);
            accountController.ModelState.AddModelError("key", "message");

            // Act
            var result = accountController.Register(registerModel) as ViewResult;

            // Assert
            Assert.AreSame(registerModel, result.Model);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldCallCreateUserMethodOfUserFactory_WhenModelStateOfControllerIsValid(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(), 
                It.IsAny<string>()))
                .Returns(new IdentityResult());
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(new User())
                .Verifiable();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            mockedUserFactory.Verify(uf =>
                uf.CreateUser(email, firstName, lastName, email), Times.Once);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldCallCreateUserMethodOfAuthenticationProvider_WhenModelStateOfControllerIsValid(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(new IdentityResult())
                .Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            mockedAuthenticationProvider.Verify(ap =>
                ap.CreateUser(user, password), Times.Once);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldAddErrors_WhenResultFromAuthenticationProviderIsNotSuccessful(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(new IdentityResult(new List<string>() { "Error" }));
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            Assert.AreEqual(1, accountController.ModelState.Count);
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldCallCreateShoppingCartMethodOfShoppingCartFactory_WhenResultFromAuthenticationProviderIsSuccess(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User() { Id="42123" };
            var shoppingCart = new ShoppingCart();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success);
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            mockedShoppingCartFactory.Setup(scf => scf.CreateShoppingCart(It.IsAny<string>(),
                It.IsAny<User>(), It.IsAny<decimal>()))
                .Verifiable();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            mockedShoppingCartFactory.Verify(scf =>
                scf.CreateShoppingCart(user.Id, user, 0M), Times.Once);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldCallAssignShoppingCartMethodOfShoppingCartsService_WhenResultFromAuthenticationProviderIsSuccess(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User() { Id = "42123" };
            var shoppingCart = new ShoppingCart();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success);
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            mockedShoppingCartFactory.Setup(scf => scf.CreateShoppingCart(It.IsAny<string>(),
                It.IsAny<User>(), It.IsAny<decimal>()))
                .Returns(shoppingCart);
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs =>
                scs.AssignShoppingCart(It.IsAny<ShoppingCart>()))
                .Verifiable();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            mockedShoppingCartsService.Verify(scs =>
                scs.AssignShoppingCart(shoppingCart), Times.Once);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldCallSignInMethodOfShoppingCartsService_WhenResultFromAuthenticationProviderIsSuccess(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User() { Id = "42123" };
            var shoppingCart = new ShoppingCart();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success);
            mockedAuthenticationProvider.Setup(ap => ap.SignIn(It.IsAny<User>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel);

            // Assert
            mockedAuthenticationProvider.Verify(scs =>
                scs.SignIn(user, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [TestCase("elon@tesla.com", "Elon", "Musk", "password")]
        [TestCase("matt@faradayfuture.com", "Matt", "Smith", "password")]
        public void ShouldReturnRedirectToRouteResultWithDefaultControllerAndAction_WhenResultFromAuthenticationProviderIsSuccess(string email,
            string firstName,
            string lastName,
            string password)
        {
            // Arrange
            var user = new User() { Id = "42123" };
            var shoppingCart = new ShoppingCart();
            var registerModel = new RegisterViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = password
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.CreateUser(It.IsAny<User>(),
                It.IsAny<string>()))
                .Returns(IdentityResult.Success);
            mockedAuthenticationProvider.Setup(ap => ap.SignIn(It.IsAny<User>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            mockedUserFactory.Setup(uf => uf.CreateUser(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Register(registerModel) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
