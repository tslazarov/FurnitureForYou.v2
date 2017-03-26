using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class SearchProducts
    {
        [Test]
        public void ShouldCallSearchProductsMethodOfProductsService()
        {
            // Arrange
            var page = 1;
            var productsPerPage = 10;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products)
                .Verifiable();
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            mockedProductsService.Verify(ps =>
                ps.SearchProducts(searchModel.SearchWord,
                    searchModel.SortBy,
                    page,
                    productsPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallGetProductsCountMethodOfProductsService()
        {
            // Arrange
            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count)
                .Verifiable();
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
            productManagementController.SearchProducts(searchModel, productsViewModel, null);

            // Assert
            mockedProductsService.Verify(ps =>
                ps.GetProductsCount(searchModel.SearchWord), Times.Once);
        }

        [Test]
        public void ShouldSetSearchModelOfProductsViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count); ;
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            Assert.AreSame(searchModel, productsViewModel.SearchModel);
        }

        [Test]
        public void ShouldSetProductsCountOfProductsViewModel()
        {
            // Arrange
            var page = 1;
            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            Assert.AreEqual(products.Count, productsViewModel.ProductsCount);
        }

        [Test]
        public void ShouldSetPagesOfProductsViewModel()
        {
            // Arrange
            var productsPerPage = 10;
            var page = 1;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var expectedPages = (int)Math.Ceiling((double)products.Count / productsPerPage);

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, productsViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfProductsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            Assert.AreEqual(page, productsViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetProductsOfProductsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var singleProducts = new List<SingleProductViewModel>();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()))
                .Returns(singleProducts);
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.SearchProducts(searchModel, productsViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleProducts, productsViewModel.Products);
        }

        [Test]
        public void ShouldRenderProductsPartialViewWithProductsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var productsViewModel = new ProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleProductViewModel>>(It.IsAny<object>()));
            var mockedImageUploader = new Mock<IImageUploader>();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.SearchProducts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedProductsService.Setup(ps => ps.GetProductsCount(It.IsAny<string>()))
                .Returns(products.Count);
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
            productManagementController.WithCallTo(pmc => pmc.SearchProducts(searchModel,
                productsViewModel,
                page))
                .ShouldRenderPartialView("ProductsPartial")
                .WithModel<ProductsViewModel>(model => Assert.AreEqual(productsViewModel, model));
        }
    }
}
