using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Mappings;
using FFY.Web.Models.Furniture;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.FurnitureControllerTests
{
    [TestFixture]
    public class Rate
    {
        [Test]
        public void ShouldRedirectToLogin_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var id = 4;
            var returnUrl = $"/furniture/product/{id}";
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id }
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(false);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.WithCallTo(fc => fc.Rate(detailedProductViewModel))
                .ShouldRedirectTo((AccountController ac) => ac.Login(returnUrl));
        }

        [Test]
        public void ShouldGetCurrentUserIdPropertyOfAuthenticationProvider()
        {
            // Arrange
            var id = 4;
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id }
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Verifiable();
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(true);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Rate(detailedProductViewModel);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = 4;
            var userId = "42";
            var user = new User();
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id }
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(userId);
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(true);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Rate(detailedProductViewModel);

            // Assert
            mockedUsersService.Verify(us => us.GetUserById(userId), Times.Once);
        }

        [Test]
        public void ShouldCallGetProductByIdMethodOfProductsService()
        {
            // Arrange
            var id = 4;
            var userId = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() { UserId = "42" } };
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id }
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(userId);
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(true);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Verifiable();
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Rate(detailedProductViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetProductById(id), Times.Once);
        }

        [Test]
        public void ShouldCallAddMethodOfShoppingCartsService()
        {
            // Arrange
            var id = 4;
            var userId = "42";
            var user = new User();
            var product = new FFY.Models.Product();
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id },
                GivenRating = 5
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(userId);
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(true);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.Add(It.IsAny<ShoppingCart>(),
                It.IsAny<FFY.Models.Product>(),
                It.IsAny<int>()))
                .Verifiable();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(product);
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Rate(detailedProductViewModel);

            // Assert
            mockedUsersService.Verify(us =>
                us.RateProduct(user, product, detailedProductViewModel.GivenRating), Times.Once);
        }

        [Test]
        public void ShouldRedirectToContactActionOfFurnitureController()
        {
            // Arrange
            int count = 2;
            var id = 4;
            var user = new User() { ShoppingCart = new ShoppingCart() { UserId = "42" } };
            var detailedProductViewModel = new DetailedProductViewModel()
            {
                Product = new Models.Product() { Id = id }
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.IsAuthenticated)
                .Returns(true);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            mockedCachingProvider.Setup(cp => cp.InsertItem(It.IsAny<string>(),
                It.IsAny<object>()))
                .Verifiable();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.CartProductsCount(It.IsAny<string>()))
                .Returns(count);
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.WithCallTo(fc => fc.Rate(detailedProductViewModel))
                .ShouldRedirectTo((FurnitureController fc) => fc.Product(id, detailedProductViewModel));
        }
    }
}
