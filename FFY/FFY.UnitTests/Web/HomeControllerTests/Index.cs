using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Mappings;
using FFY.Web.Models.Furniture;
using FFY.Web.Models.Home;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.HomeControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldCallGetLatestProductsMethodOfProductsService()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetLatestProducts(It.IsAny<int>()))
                .Verifiable();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetLatestProducts(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldCallGetHighestRatedProductsMethodOfProductsService()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetHighestRatedProducts(It.IsAny<int>()))
                .Verifiable();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetHighestRatedProducts(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldCallGetDiscountProductsMethodOfProductsService()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetDiscountProducts(It.IsAny<int>()))
                .Verifiable();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            mockedProductsService.Verify(ps => ps.GetDiscountProducts(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldMapLatestProductsCollectionOfModel()
        {
            // Arrange
            var collection = new List<SingleProductSelectionViewModel>();
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductSelectionViewModel>>(It.IsAny<object>()))
                .Returns(collection);
            var mockedProductsService = new Mock<IProductsService>();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            CollectionAssert.AreEquivalent(collection, homeViewModel.LatestProducts);
        }

        [Test]
        public void ShouldMapDiscountedProductsCollectionOfModel()
        {
            // Arrange
            var collection = new List<SingleProductSelectionViewModel>();
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductSelectionViewModel>>(It.IsAny<object>()))
                .Returns(collection);
            var mockedProductsService = new Mock<IProductsService>();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            CollectionAssert.AreEquivalent(collection, homeViewModel.DiscountProducts);
        }

        [Test]
        public void ShouldMapHighestRatedProductsCollectionOfModel()
        {
            // Arrange
            var collection = new List<SingleProductSelectionViewModel>();
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleProductSelectionViewModel>>(It.IsAny<object>()))
                .Returns(collection);
            var mockedProductsService = new Mock<IProductsService>();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.Index(homeViewModel);

            // Assert
            CollectionAssert.AreEquivalent(collection, homeViewModel.HighestRatedProducts);
        }

        [Test]
        public void ShouldReturnDefaultViewWithHomeViewModel()
        {
            // Arrange
            var homeViewModel = new HomeViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedProductsService = new Mock<IProductsService>();
            mockedProductsService.Setup(ps => ps.GetDiscountProducts(It.IsAny<int>()))
                .Verifiable();

            var homeController = new HomeController(mockedMapperProvider.Object,
                    mockedProductsService.Object);

            // Act
            homeController.WithCallTo(hc => hc.Index(homeViewModel))
                .ShouldRenderDefaultView()
                .WithModel<HomeViewModel>(model => Assert.AreEqual(homeViewModel, model));
        }
    }
}
