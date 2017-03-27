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
    public class Index
    {
        [Test]
        public void ShouldCallGetRoomsMethodOfRoomsService()
        {
            // Arrange
            var roomsViewModel = new RoomsViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRooms()).Verifiable();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Index(roomsViewModel);

            // Assert
            mockedRoomsService.Verify(rs => rs.GetRooms(), Times.Once);
        }

        [Test]
        public void ShouldReturnDefaultViewWithRoomsViewModel()
        {
            // Arrange
            var roomsViewModel = new RoomsViewModel();

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

            // Act and Assert
            furnitureController.WithCallTo(fc => fc.Index(roomsViewModel))
                .ShouldRenderDefaultView()
                .WithModel<RoomsViewModel>(model => Assert.AreEqual(roomsViewModel, model));
        }
    }
}
