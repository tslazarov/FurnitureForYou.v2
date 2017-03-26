using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class ProductAddition
    {

        [Test]
        public void ShouldCallGetRoomsMethodOfRoomsService()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRooms())
                .Verifiable();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            var productManagementController = new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object);

            // Act
            productManagementController.ProductAddition();

            // Assert
            mockedRoomsService.Verify(rs => rs.GetRooms(), Times.Once);
        }

        [Test]
        public void ShouldCallGetCategoriesMethodOfCategoriesService()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategories())
                .Verifiable();

            var productManagementController = new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object);
            
            // Act
            productManagementController.ProductAddition();

            // Assert
            mockedCategoriesService.Verify(cs => cs.GetCategories(), Times.Once);
        }


        [Test]
        public void ShouldReturnProductAdditionView()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            var productManagementController = new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object);

            // Act and Assert
            productManagementController.WithCallTo(pmc => pmc.ProductAddition())
                .ShouldRenderView("ProductOperation");
        }
    }
}
