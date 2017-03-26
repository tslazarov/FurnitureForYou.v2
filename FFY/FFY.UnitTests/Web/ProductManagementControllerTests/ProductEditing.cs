using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class ProductEditing
    {
        [Test]
        public void ShouldCallGetRoomsMethodOfRoomsService()
        {
            // Arrange
            var productOperationViewModel = new ProductOperationViewModel();

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
            productManagementController.ProductEditing(1, productOperationViewModel);

            // Assert
            mockedRoomsService.Verify(rs => rs.GetRooms(), Times.Once);
        }

        [Test]
        public void ShouldCallGetCategoriesMethodOfCategoriesService()
        {
            // Arrange
            var productOperationViewModel = new ProductOperationViewModel();

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
            productManagementController.ProductEditing(1, productOperationViewModel);

            // Assert
            mockedCategoriesService.Verify(cs => cs.GetCategories(), Times.Once);
        }

        [Test]
        public void ShouldAssignPropertiesToModelFromProduct()
        {
            // Arrange
            var product = new Product()
            {
                Id = 1,
                Name = "Product",
                Price = 12,
                DiscountPercentage = 10,
                Quantity = 5,
                Description = "Description",
                RoomId = 1,
                CategoryId = 2,
                ImagePath = "path"
            };
            var productOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(product);
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

            // Act
            productManagementController.ProductEditing(1, productOperationViewModel);

            // Assert
            Assert.AreEqual(product.Id, productOperationViewModel.Id);
            Assert.AreEqual(product.Name, productOperationViewModel.Name);
            Assert.AreEqual(product.Price, productOperationViewModel.Price);
            Assert.AreEqual(product.Quantity, productOperationViewModel.Quantity);
            Assert.AreEqual(product.DiscountPercentage, productOperationViewModel.DiscountPercentage);
            Assert.AreEqual(product.Description, productOperationViewModel.Description);
            Assert.AreEqual(product.RoomId, productOperationViewModel.RoomId);
            Assert.AreEqual(product.CategoryId, productOperationViewModel.CategoryId);
            Assert.AreEqual(product.ImagePath, productOperationViewModel.ImagePath);
        }

        [Test]
        public void ShouldReturnPageNotFoundView_WhenProductIsNotFound()
        {
            // Arrange
            var productOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()));
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
            productManagementController.WithCallTo(pmc => pmc.ProductEditing(1, productOperationViewModel))
                .ShouldRenderView("NotFound");
        }

        [Test]
        public void ShouldReturnProductOperationView_WhenProductIsFound()
        {
            // Arrange
            var productOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product() { RoomId = 1, CategoryId = 2 });
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
            productManagementController.WithCallTo(pmc => pmc.ProductEditing(1, productOperationViewModel))
                .ShouldRenderView("ProductOperation");
        }
    }
}
