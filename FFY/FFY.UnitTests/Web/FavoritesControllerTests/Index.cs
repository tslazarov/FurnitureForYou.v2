using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.FavoritesControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultViewWithFavoriteProductsViewModel()
        {
            // Arrange
            var favoriteProductsViewModel = new FavoriteProductsViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            var favoritesController = new FavoritesController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act and Assert
            favoritesController.WithCallTo(fc => fc.Index(favoriteProductsViewModel))
                .ShouldRenderDefaultView()
                .WithModel<FavoriteProductsViewModel>(model => Assert.AreEqual(favoriteProductsViewModel, model));
        }
    }
}
