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
    public class Product
    {
        [Test]
        public void ShouldCallGetProductByIdMethodOfProductsService()
        {
            // Arrange
            var id = 1;
            var detailedProductViewModel = new DetailedProductViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
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
            furnitureController.Product(id, detailedProductViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetProductById(id), Times.Once);
        }

        [Test]
        public void ShouldReturnNotFoundView_WhenProductIsNotFound()
        {
            // Arrange
            var id = 1;
            var detailedProductViewModel = new DetailedProductViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
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
            furnitureController.WithCallTo(fc => fc.Product(id, detailedProductViewModel))
                .ShouldRenderView("NotFound");
        }

        [Test]
        public void ShouldReturnDefaultViewWithDetailedProductViewModel()
        {
            // Arrange
            var id = 1;
            var product = new FFY.Models.Product();
            var detailedProductViewModel = new DetailedProductViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
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
            furnitureController.WithCallTo(fc => fc.Product(id, detailedProductViewModel))
                .ShouldRenderDefaultView()
                .WithModel<DetailedProductViewModel>(model => Assert.AreEqual(detailedProductViewModel, model));
        }
    }
}
