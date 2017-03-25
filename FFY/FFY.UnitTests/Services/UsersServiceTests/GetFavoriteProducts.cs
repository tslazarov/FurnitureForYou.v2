using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetFavoriteProducts
    {
        [Test]
        public void ShouldThrowArgumentException_WhenEmptyUserIdIsPassed()
        {
            // Arrange
            var id = "";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                usersService.GetFavoriteProducts(id, 1, 16));
        }

        [Test]
        public void ShouldThrowArgumentExceptionWithCorrectMessage_WhenEmptyUserIdIsPassed()
        {
            // Arrange
            var id = "";
            var expectedExMessage = "User id cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                usersService.GetFavoriteProducts(id, 1, 16));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserIdIsPassed()
        {
            // Arrange
            string id = null;
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                usersService.GetFavoriteProducts(id, 1, 16));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserIdIsPassed()
        {
            // Arrange
            string id = null;
            var expectedExMessage = "User id cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                usersService.GetFavoriteProducts(id, 1, 16));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallGetByIdMethodOfDataUsersRepository()
        {
            // Arrange
            var id = "42";

            var user = new User() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.GetFavoriteProducts(id, 1, 16);

            // Assert
            mockedData.Verify(d => d.UsersRepository.GetById(id), Times.Once);
        }

        [Test]
        public void ShouldReturnFavoriteProducts()
        {
            // Arrange
            var id = "42";
            var favoritedProducts = new List<Product>() {
                    new Product() { Name = "Bed" },
                    new Product() { Name = "Chair" },
                    new Product() { Name = "Table" }
                };

            var user = new User()
            {
                Id = id,
                FavoritedProducts = favoritedProducts
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetFavoriteProducts(id, 1, 16);

            // Assert
            CollectionAssert.AreEquivalent(favoritedProducts, result);
        }

        [Test]
        public void ShouldReturnFavoriteProductsBaseOnPage()
        {
            // Arrange
            var id = "42";
            var favoritedProducts = new List<Product>() {
                    new Product() { Name = "Bed" },
                    new Product() { Name = "Chair" },
                    new Product() { Name = "Table" },
                    new Product() { Name = "Wardrobe" }
                };

            var user = new User()
            {
                Id = id,
                FavoritedProducts = favoritedProducts
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetFavoriteProducts(id, 2, 2);

            // Assert
            Assert.AreSame(favoritedProducts[2], result.First());
            Assert.AreSame(favoritedProducts[3], result.Last());
        }
    }
}
