using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FFY.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetFavoriteProductsCount
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
                usersService.GetFavoriteProductsCount(id));
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
                usersService.GetFavoriteProductsCount(id));
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
                usersService.GetFavoriteProductsCount(id));
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
                usersService.GetFavoriteProductsCount(id));
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
            usersService.GetFavoriteProductsCount(id);

            // Assert
            mockedData.Verify(d => d.UsersRepository.GetById(id), Times.Once);
        }

        [Test]
        public void ShouldReturnFavoriteProductsCountOfUser_WhenSuchUserIsFound()
        {
            // Arrange
            var id = "42";
            var favoritedProducts = new List<Product>() {
                    new Product(),
                    new Product(),
                    new Product()
                };

            var user = new User() {
                Id = id,
                FavoritedProducts = favoritedProducts
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetFavoriteProductsCount(id);

            // Assert
            Assert.AreEqual(favoritedProducts.Count, result);
        }
    }
}
