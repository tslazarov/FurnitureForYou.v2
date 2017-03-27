using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Mappings;
using FFY.Web.Models.Furniture;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.FurnitureControllerTests
{
    [TestFixture]
    public class Products
    {
        [Test]
        public void ShouldCallGetProductsSelectionMethodOfProductsService()
        {
            // Arrange
            var page = 1;
            var productsSelectionViewModel = new ProductsSelectionViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductsSelection(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
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
            furnitureController.Products(productsSelectionViewModel, "", "", null, null, page);

            // Assert
            mockedProductsService.Verify(ps => ps.GetProductsSelection(It.IsAny<string>(),
                It.IsAny<string>(),
                null,
                null,
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldCallGetProductsSelectionCountMethodOfProductsService()
        {
            // Arrange
            var page = 1;
            var productsSelectionViewModel = new ProductsSelectionViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductsSelectionCount(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>()))
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
            furnitureController.Products(productsSelectionViewModel, "", "", null, null, page);

            // Assert
            mockedProductsService.Verify(ps => ps.GetProductsSelectionCount(It.IsAny<string>(),
                It.IsAny<string>(),
                null,
                null), Times.Once);
        }

        [Test]
        public void ShouldAssignPropertiesToProductsSelectionViewModel()
        {
            // Arrange
            var productsPerPage = 16;
            var filterBy = "all";
            var page = 1;
            var search = "search";
            var from = 1;
            var to = 100;
            var count = 5;
            var expectedPages = (int)Math.Ceiling((double)count / productsPerPage);

            var productsSelectionViewModel = new ProductsSelectionViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductsSelectionCount(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(count);
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Products(productsSelectionViewModel, filterBy, search, from, to, page);

            // Assert
            Assert.AreEqual(search, productsSelectionViewModel.Search);
            Assert.AreEqual(filterBy, productsSelectionViewModel.FilterBy);
            Assert.AreEqual(from, productsSelectionViewModel.From);
            Assert.AreEqual(to, productsSelectionViewModel.To);
            Assert.AreEqual(count, productsSelectionViewModel.ProductsCount);
            Assert.AreEqual(expectedPages, productsSelectionViewModel.Pages);
            Assert.AreEqual(page, productsSelectionViewModel.Page);
        }

        [Test]
        public void ShouldMapSingleProductSelectionViewModelCollection()
        {
            // Arrange
            var singleProductSelection = new List<SingleProductSelectionViewModel>();
            var productsSelectionViewModel = new ProductsSelectionViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            mockedMapper.Setup(m => m.Map<IEnumerable<SingleProductSelectionViewModel>>(It.IsAny<object>()))
                .Returns(singleProductSelection);
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductsSelectionCount(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()));
            var mockedRoomsService = new Mock<IRoomsService>();

            var furnitureController = new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object);

            // Act
            furnitureController.Products(productsSelectionViewModel, "", "", null, null, 1);

            // Assert
            CollectionAssert.AreEquivalent(singleProductSelection, productsSelectionViewModel.Products);
        }

        [Test]
        public void ShouldReturnDefaultViewWithProductsSelectionViewModel()
        {
            // Arrange
            var productsSelectionViewModel = new ProductsSelectionViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetProductsSelection(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
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
            furnitureController.WithCallTo(fc => fc.Products(productsSelectionViewModel, null, null, null, null, null))
                .ShouldRenderDefaultView()
                .WithModel<ProductsSelectionViewModel>(model => Assert.AreEqual(productsSelectionViewModel, model));
        }
    }
}
