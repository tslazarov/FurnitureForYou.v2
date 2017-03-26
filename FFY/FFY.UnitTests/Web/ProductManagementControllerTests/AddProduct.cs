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
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class AddProduct
    {
        [Test]
        public void ShouldCallUploadMethodOfImageUploader()
        {
            // Arrange
            var imageFileName = "default-product-image.jpg";
            var folderName = "products";

            var productsOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
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
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room());
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category());

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
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedImageUploader.Verify(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                imageFileName,
                folderName), Times.Once);
        }

        [Test]
        public void ShouldCallGetRoomsByIdMethodOfRoomsService()
        {
            // Arrange
            var id = 2;

            var productsOperationViewModel = new ProductOperationViewModel()
            {
                RoomId = id
            };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room())
                .Verifiable();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category());

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
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedRoomsService.Verify(rs => rs.GetRoomById(id), Times.Once);
        }

        [Test]
        public void ShouldCallGetCategoryByIdMethodOfRoomsService()
        {
            // Arrange
            var id = 2;

            var productsOperationViewModel = new ProductOperationViewModel()
            {
                CategoryId = id
            };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room());
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category())
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
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedCategoriesService.Verify(cs => cs.GetCategoryById(id), Times.Once);
        }

        [Test]
        public void ShouldCallCreateProductMethodOfProductsFactory()
        {
            // Arrange
            var roomId = 2;
            var categoryId = 4;
            var imagePath = "image-path";
            var room = new Room() { Id = roomId };
            var category = new Category() { Id = categoryId };

            var productsOperationViewModel = new ProductOperationViewModel()
            {
                Name = "Product",
                Quantity = 1,
                Price = 15,
                DiscountPercentage = 10,
                Description = "Description"
            };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            mockedImageUploader.Setup(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(imagePath);
            mockedImageUploader.Setup(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                It.IsAny<string>(),
                It.IsAny<string>()));
            var mockedProductFactory = new Mock<IProductFactory>();
            mockedProductFactory.Setup(pf => pf.CreateProduct(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<Category>(),
                It.IsAny<int>(),
                It.IsAny<Room>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
                .Verifiable();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(room);
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(category); ;

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
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedProductFactory.Verify(pf => pf.CreateProduct(productsOperationViewModel.Name,
                productsOperationViewModel.Quantity,
                productsOperationViewModel.Price,
                productsOperationViewModel.DiscountedPrice,
                productsOperationViewModel.DiscountPercentage,
                true,
                productsOperationViewModel.Description,
                category.Id,
                category,
                room.Id,
                room,
                It.IsAny<string>(),
                false), Times.Once);
        }

        [Test]
        public void ShouldCallAddProductMethodOfProductsService()
        {
            // Arrange
            var product = new Product();
            var productsOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            mockedProductFactory.Setup(pf => pf.CreateProduct(It.IsAny<string>(),
                 It.IsAny<int>(),
                 It.IsAny<decimal>(),
                 It.IsAny<decimal>(),
                 It.IsAny<int>(),
                 It.IsAny<bool>(),
                 It.IsAny<string>(),
                 It.IsAny<int>(),
                 It.IsAny<Category>(),
                 It.IsAny<int>(),
                 It.IsAny<Room>(),
                 It.IsAny<string>(),
                 It.IsAny<bool>()))
                 .Returns(product);
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.AddProduct(It.IsAny<Product>()))
                .Verifiable();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room());
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category());

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
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.AddProduct(product), Times.Once);
        }

        [Test]
        public void ShouldRedirectToIndexActionOfProductManagement()
        {
            // Arrange
            var productsOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room());
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category());

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
            productManagementController.WithCallTo(pmc => pmc.AddProduct(productsOperationViewModel))
                .ShouldRedirectTo((ProductManagementController pmc) => pmc.Index(It.IsAny<ProductsViewModel>()));
        }
    }
}
