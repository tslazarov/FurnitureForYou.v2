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
    public class AddCategory
    {
        [Test]
        public void ShouldCallUploadMethodOfImageUploader()
        {
            // Arrange
            var imageFileName = "default-category-image.jpg";
            var folderName = "categories";

            var categoryPartialViewModel = new CategoryPartialViewModel();

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
            productManagementController.AddCategory(categoryPartialViewModel);

            // Assert
            mockedImageUploader.Verify(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                imageFileName,
                folderName), Times.Once);
        }

        [Test]
        public void ShouldCallCreateCategoryMethodOfCategoryFactory()
        {
            // Arrange
            var imagePath = "image-path";

            var categoryPartialViewModel = new CategoryPartialViewModel()
            {
                Name = "Tables"
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
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            mockedCategoryFactory.Setup(cf => cf.CreateCategory(It.IsAny<string>(),
                It.IsAny<string>())).Verifiable();
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
            productManagementController.AddCategory(categoryPartialViewModel);

            // Assert
            mockedCategoryFactory.Verify(cf => cf.CreateCategory(categoryPartialViewModel.Name,
                imagePath), Times.Once);
        }

        [Test]
        public void ShouldCallAddCategoryMethodOfCategoriesService()
        {
            // Arrange
            var category = new Category();
            var categoryPartialViewModel = new CategoryPartialViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            mockedCategoryFactory.Setup(cf => cf.CreateCategory(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(category);
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.AddCategory(It.IsAny<Category>()))
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
            productManagementController.AddCategory(categoryPartialViewModel);

            // Assert
            mockedCategoriesService.Verify(cs => cs.AddCategory(category), Times.Once);
        }

        [Test]
        public void ShouldRedirectToIndexActionOfProductManagement()
        {
            // Arrange
            var categoryPartialViewModel = new CategoryPartialViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
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
            productManagementController.WithCallTo(pmc => pmc.AddCategory(categoryPartialViewModel))
                .ShouldRedirectTo((ProductManagementController pmc) => pmc.ProductAddition());
        }
    }
}
