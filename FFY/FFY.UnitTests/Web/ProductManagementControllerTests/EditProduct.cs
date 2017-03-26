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
    public class EditProduct
    {
        [Test]
        public void ShouldCallGetProductByIdMethodOfProductsService()
        {
            // Arrange
            var id = 2;

            var productsOperationViewModel = new ProductOperationViewModel()
            {
                Id = id
            };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.Setup(rp => rp.GetRequestFiles(It.IsAny<Controller>()))
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product())
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
            productManagementController.EditProduct(productsOperationViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetProductById(id), Times.Once);
        }

        [Test]
        public void ShouldCallUploadMethodOfImageUploader()
        {
            // Arrange
            var imageFileName = "default-product-image.jpg";
            var folderName = "products";

            var productsOperationViewModel = new ProductOperationViewModel()
            {
                ImagePath = imageFileName
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
                .Verifiable();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product());
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
            productManagementController.EditProduct(productsOperationViewModel);

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
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product());
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
            productManagementController.EditProduct(productsOperationViewModel);

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
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product());
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
            productManagementController.EditProduct(productsOperationViewModel);

            // Assert
            mockedCategoriesService.Verify(cs => cs.GetCategoryById(id), Times.Once);
        }


        [Test]
        public void ShouldAssignPropertiesToProductFromProductsOperationViewModel()
        {
            // Arrange
            var imagePath = "path";
            var room = new Room();
            var category = new Category();
            var product = new Product();
            var productsOperationViewModel = new ProductOperationViewModel()
            {
                Name = "Product",
                Price = 10,
                DiscountPercentage = 10,
                Quantity = 10,
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
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(product);
            mockedProductsService.Setup(ps => ps.UpdateProduct(It.IsAny<Product>()));
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(room);
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(category);

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
            productManagementController.EditProduct(productsOperationViewModel);

            // Assert
            Assert.AreEqual(productsOperationViewModel.Name, product.Name);
            Assert.AreEqual(productsOperationViewModel.Price, product.Price);
            Assert.AreEqual(productsOperationViewModel.DiscountPercentage, product.DiscountPercentage);
            Assert.AreEqual(productsOperationViewModel.DiscountedPrice, product.DiscountedPrice);
            Assert.AreEqual(productsOperationViewModel.Quantity, product.Quantity);
            Assert.AreEqual(productsOperationViewModel.Description, product.Description);
            Assert.AreEqual(imagePath, product.ImagePath);
            Assert.AreSame(room, product.Room);
            Assert.AreSame(category, product.Category);
            Assert.AreEqual(true, product.HasDiscount);
        }

        [Test]
        public void ShouldCallUpdateProductMethodOfProductsService()
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
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(product);
            mockedProductsService.Setup(ps => ps.UpdateProduct(It.IsAny<Product>()))
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
            productManagementController.EditProduct(productsOperationViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.UpdateProduct(product), Times.Once);
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
            mockedProductsService.Setup(ps => ps.GetProductById(It.IsAny<int>()))
                .Returns(new Product());
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room()); ;
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
            productManagementController.WithCallTo(pmc => pmc.EditProduct(productsOperationViewModel))
                .ShouldRedirectTo((ProductManagementController pmc) => pmc.Index(It.IsAny<ProductsViewModel>()));
        }
    }
}
