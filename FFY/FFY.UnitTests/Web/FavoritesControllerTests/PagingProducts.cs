using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.FavoritesControllerTests
{
    [TestFixture]
    public class PagingProducts
    {
        [Test]
        public void ShouldGetCurrentUserIdOfAuthenticationProvider()
        {
            // Arrange
            var page = 1;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var products = new List<Product>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id)
                .Verifiable();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldCallGetFavoriteProductsMethodOfUsersService()
        {
            // Arrange
            var page = 1;
            var productsPerPage = 16;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var products = new List<Product>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products)
                .Verifiable();
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            mockedUsersService.Verify(us => 
                us.GetFavoriteProducts(id, page, productsPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallGetFavoriteProductsCountMethodOfUsersService()
        {
            // Arrange
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var products = new List<Product>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count)
                .Verifiable();

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, null);

            // Assert
            mockedUsersService.Verify(us =>
                us.GetFavoriteProductsCount(id), Times.Once);
        }

        [Test]
        public void ShouldSetFavoriteProductsCountOfFavoriteProductsViewModel()
        {
            // Arrange
            var page = 1;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            Assert.AreEqual(products.Count, favoriteProductsViewModel.FavoriteProductsCount);
        }

        [Test]
        public void ShouldSetPagesOfFavoriteProductsViewModel()
        {
            // Arrange
            var productsPerPage = 16;
            var page = 1;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var expectedPages = (int)Math.Ceiling((double)products.Count / productsPerPage);

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, favoriteProductsViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfFavoriteProductsViewModel()
        {
            // Arrange
            var page = 3;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            Assert.AreEqual(page, favoriteProductsViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetFavoriteProductsOfFavoriteProductsViewModel()
        {
            // Arrange
            var page = 3;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var singleFavoriteProducts = new List<SingleFavoriteProductViewModel>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()))
                .Returns(singleFavoriteProducts);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act 
            favoritesController.PagingProducts(favoriteProductsViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleFavoriteProducts, favoriteProductsViewModel.FavoriteProducts);
        }

        [Test]
        public void ShouldRenderFavoriteProductsPartialViewWithFavoriteProductsViewModel()
        {
            // Arrange
            var page = 3;
            var id = "10";

            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var product = new Product();
            var products = new List<Product>() { product };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleFavoriteProductViewModel>>(It.IsAny<object>()));
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetFavoriteProducts(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(products);
            mockedUsersService.Setup(us => us.GetFavoriteProductsCount(It.IsAny<string>()))
                .Returns(products.Count);

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUsersService.Object);

            // Act and Assert
            favoritesController.WithCallTo(fc => fc.PagingProducts(favoriteProductsViewModel,
                page))
                .ShouldRenderPartialView("FavoriteProductsPartial")
                .WithModel<FavoriteProductsViewModel>(model => Assert.AreEqual(favoriteProductsViewModel, model));
        }
    }
}
