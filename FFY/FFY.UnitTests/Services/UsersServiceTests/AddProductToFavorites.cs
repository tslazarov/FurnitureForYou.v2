using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class AddProductToFavorites
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedProduct = new Mock<Product>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => usersService.AddProductToFavorites(null, mockedProduct.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserIsPassed()
        {
            // Arrange
            var expectedExMessage = "User cannot be null.";

            var mockedData = new Mock<IFFYData>();
            var mockedProduct = new Mock<Product>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                usersService.AddProductToFavorites(null, mockedProduct.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedUser = new Mock<User>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => usersService.AddProductToFavorites(mockedUser.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductIsPassed()
        {
            // Arrange
            var expectedExMessage = "Product cannot be null.";

            var mockedData = new Mock<IFFYData>();
            var mockedUser = new Mock<User>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                usersService.AddProductToFavorites(mockedUser.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldAddProductToUsersFavoriteProductsCollection()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.FavoritedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Favoriters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.AddProductToFavorites(mockedUser.Object, mockedProduct.Object);

            // Assert
            Assert.AreEqual(1, mockedUser.Object.FavoritedProducts.Count);
            Assert.AreSame(mockedProduct.Object, mockedUser.Object.FavoritedProducts.First());
        }

        [Test]
        public void ShouldAddUserToProductFavoritersCollection()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.FavoritedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Favoriters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.AddProductToFavorites(mockedUser.Object, mockedProduct.Object);

            // Assert
            Assert.AreEqual(1, mockedProduct.Object.Favoriters.Count);
            Assert.AreSame(mockedUser.Object, mockedProduct.Object.Favoriters.First());
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataUsersRepository()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>())).Verifiable();
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.FavoritedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Favoriters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.AddProductToFavorites(mockedUser.Object, mockedProduct.Object);

            // Assert
            mockedData.Verify(d => d.UsersRepository.Update(mockedUser.Object), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataProductsRepository()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>())).Verifiable();

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.FavoritedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Favoriters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.AddProductToFavorites(mockedUser.Object, mockedProduct.Object);

            // Assert
            mockedData.Verify(d => d.ProductsRepository.Update(mockedProduct.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.FavoritedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Favoriters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.AddProductToFavorites(mockedUser.Object, mockedProduct.Object);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
