using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRequestProviderIsPassed()
        {
            // Arrange
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(null,
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRequestProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Request provider cannot be null.";

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(null,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        null,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    null,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullImageUploaderIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        null,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullImageUploaderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Image uploader cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    mockedMapperProvider.Object,
                    null,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductFactoryIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        null,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Product factory cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    null,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductsServiceIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        null,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Products service cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    null,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRoomFactoryIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();


            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        null,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRoomFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Room factory cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    null,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRoomsServiceIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        null,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRoomsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Rooms service cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    null,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCategoryFactoryIsPassed()
        {
            // Arrange
            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        null,
                        mockedCategoriesService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCategoryFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Category factory cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoriesService = new Mock<ICategoriesService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object, 
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    null,
                    mockedCategoriesService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCategoriesServiceIsPassed()
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

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ProductManagementController(mockedRequestProvider.Object, 
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCategoriesServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Categories service cannot be null.";

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
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

            // Act and Assert
            Assert.DoesNotThrow(() =>
                    new ProductManagementController(mockedRequestProvider.Object,
                        mockedMapperProvider.Object,
                        mockedImageUploader.Object,
                        mockedProductFactory.Object,
                        mockedProductsService.Object,
                        mockedRoomFactory.Object,
                        mockedRoomsService.Object,
                        mockedCategoryFactory.Object,
                        mockedCategoriesService.Object));
        }
    }
}
