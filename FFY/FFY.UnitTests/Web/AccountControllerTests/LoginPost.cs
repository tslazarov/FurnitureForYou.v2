using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Models.Account;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.UnitTests.Web.AccountControllerTests
{
    [TestFixture]
    public class LoginPost
    {
        [TestCase("elon@tesla.com", "password", true)]
        [TestCase("elon@spacex.com", "drowssap", false)]
        public void ShouldReturnViewWithModel_WhenModelStateOfControllerIsNotValid(string email, 
            string password, 
            bool rememberMe)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            accountController.ModelState.AddModelError("key", "message"); 

            // Act
            var result = accountController.Login(loginModel, "") as ViewResult;

            // Assert
            Assert.AreSame(loginModel, result.Model);
        }

        [TestCase("elon@tesla.com", "password", true, "home")]
        [TestCase("elon@spacex.com", "drowssap", false, "nothome")]
        public void ShouldCallGetRouteDataMethodOfRouteDataProvider_WhenModelStateOfControllerIsValid(string email,
            string password,
            bool rememberMe,
            string returnUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData)
                .Verifiable();
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()));
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);

            // Act
            var result = accountController.Login(loginModel, returnUrl);

            // Assert
            mockedRouteDataProvider.Verify(rdp =>
                rdp.GetRouteData(accountController), Times.Once);
        }

        [TestCase("elon@tesla.com", "password", true, "home")]
        [TestCase("elon@spacex.com", "drowssap", false, "nothome")]
        public void ShouldCallSignInWithPasswordOfAuthenticationProvider_WhenModelStateOfControllerIsValid(string email, 
            string password, 
            bool rememberMe, 
            string returnUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Verifiable();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl);

            // Assert
            mockedAuthenticationProvider.Verify(ap => 
                ap.SignInWithPassword(email, password, rememberMe, It.IsAny<bool>()), Times.Once);
        }

        [TestCase("elon@tesla.com", "password", true, "/home", "/en/home")]
        [TestCase("elon@spacex.com", "drowssap", false, "/nothome", "/en/nothome")]
        public void ShouldReturnRedirectResultWithReturnUrl_WhenResultFromSignInWithPasswordIsSuccess(string email, 
            string password, 
            bool rememberMe, 
            string returnUrl,
            string expectedUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Success);
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl) as RedirectResult;

            // Assert
            Assert.AreEqual(expectedUrl, result.Url);
        }

        [TestCase("elon@tesla.com", "password", true, null)]
        [TestCase("elon@spacex.com", "drowssap", false, "")]
        public void ShouldReturnRedirectResultWithDefaultUrl_WhenResultFromSignInWithPasswordIsSuccessAndReturnUrlIsNullOrEmpty(string email, 
            string password, 
            bool rememberMe, 
            string returnUrl)
        {
            // Arrange
            var expectedUrl = "/en";
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Success);
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl) as RedirectResult;

            // Assert
            Assert.AreEqual(expectedUrl, result.Url);
        }

        [TestCase("elon@tesla.com", "password", true, "home")]
        public void ShouldReturnViewResultWithLockoutName_WhenResultFromSignInWithPasswordIsLockout(string email,
            string password,
            bool rememberMe,
            string returnUrl)
        {
            // Arrange
            var expectedLockOutName = "Lockout";
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.LockedOut);
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl) as ViewResult;

            // Assert
            Assert.AreEqual(expectedLockOutName, result.ViewName);
        }

        [TestCase("elon@tesla.com", "password", true, "home")]
        public void ShouldAddModelError_WhenResultFromSignInWithPasswordIsFailure(string email,
            string password,
            bool rememberMe,
            string returnUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl);

            // Assert
            Assert.IsFalse(accountController.ModelState.IsValid);
        }

        [TestCase("elon@tesla.com", "password", true, "home")]
        public void ShouldReturnViewResultWithModel_WhenResultFromSignInWithPasswordIsFailure(string email,
           string password,
           bool rememberMe,
           string returnUrl)
        {
            // Arrange
            var loginModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };
            var routeData = new RouteData();
            routeData.Values.Add("language", "en");

            var mockedRouteDataProvider = new Mock<IRouteDataProvider>();
            mockedRouteDataProvider.Setup(rdp => rdp.GetRouteData(It.IsAny<Controller>()))
                .Returns(routeData);
            var mockedAuthenticationProvider = new Mock<IHttpContextAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(ap => ap.SignInWithPassword(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            var accountController = new AccountController(mockedRouteDataProvider.Object,
                mockedAuthenticationProvider.Object,
                mockedUserFactory.Object,
                mockedShoppingCartFactory.Object,
                mockedShoppingCartsService.Object);
            
            // Act
            var result = accountController.Login(loginModel, returnUrl) as ViewResult;

            // Assert
            Assert.AreSame(loginModel, result.Model);
        }
    }
}
