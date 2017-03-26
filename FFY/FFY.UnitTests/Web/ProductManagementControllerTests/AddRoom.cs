using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.UnitTests.Web.ProductManagementControllerTests.Mocks;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System.Web;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class AddRoom
    {
        [Test]
        public void ShouldCallUploadMethodOfImageUploader()
        {
            // Arrange
            var imageFileName = "default-room-image.jpg";
            var folderName = "rooms";

            var roomPartialViewModel = new RoomPartialViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            mockedImageUploader.Setup(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Verifiable();
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

            // Act
            productManagementController.AddRoom(roomPartialViewModel);

            // Assert
            mockedImageUploader.Verify(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                imageFileName,
                folderName), Times.Once);
        }

        [Test]
        public void ShouldCallCreateRoomMethodOfRoomFactory()
        {
            // Arrange
            var imagePath = "image-path";

            var roomPartialViewModel = new RoomPartialViewModel() {
                Name = "Kitchen"
            };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            mockedImageUploader.Setup(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(imagePath);
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            mockedRoomFactory.Setup(rf => rf.CreateRoom(It.IsAny<string>(),
                It.IsAny<string>())).Verifiable();
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
            productManagementController.AddRoom(roomPartialViewModel);

            // Assert
            mockedRoomFactory.Verify(rf => rf.CreateRoom(roomPartialViewModel.Name,
                imagePath), Times.Once);
        }

        [Test]
        public void ShouldCallAddRoomMethodOfRoomsService()
        {
            // Arrange
            var room = new Room();
            var roomPartialViewModel = new RoomPartialViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            mockedRoomFactory.Setup(rf => rf.CreateRoom(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(room);
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.AddRoom(It.IsAny<Room>()))
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
            productManagementController.AddRoom(roomPartialViewModel);

            // Assert
            mockedRoomsService.Verify(rs => rs.AddRoom(room), Times.Once);
        }

        [Test]
        public void ShouldRedirectToIndexActionOfProductManagement()
        {
            // Arrange
            var roomPartialViewModel = new RoomPartialViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            mockedRoomFactory.Setup(rf => rf.CreateRoom(It.IsAny<string>(),
                It.IsAny<string>()));
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
            productManagementController.WithCallTo(pmc => pmc.AddRoom(roomPartialViewModel))
                .ShouldRedirectTo((ProductManagementController pmc) => pmc.ProductAddition());
        }
    }
}
