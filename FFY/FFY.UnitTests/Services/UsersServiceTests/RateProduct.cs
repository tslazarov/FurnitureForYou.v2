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
    public class RateProduct
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedProduct = new Mock<Product>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => 
                usersService.RateProduct(null, mockedProduct.Object, 5));
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
                usersService.RateProduct(null, mockedProduct.Object, 5));
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
            Assert.Throws<ArgumentNullException>(() => 
                usersService.RateProduct(mockedUser.Object, null, 5));
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
                usersService.RateProduct(mockedUser.Object, null, 5));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase(3, 20, 6)]
        [TestCase(4, 40, 9)]
        [TestCase(1, 20, 9)]
        public void ShouldUpdateProductRatingInformation(int givenRating, decimal currentRating, int ratingCount)
        {
            // Arrange

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var product = new Product() {
                Rating = currentRating,
                RatingCount = ratingCount
            };

           var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, product, givenRating);
            var expectedRating = (currentRating * ratingCount + givenRating) / (ratingCount + 1);
            var expectedRatingCount = ratingCount + 1;

            // Assert
            Assert.AreEqual(expectedRating, product.Rating);
            Assert.AreEqual(expectedRatingCount, product.RatingCount);
        }

        [Test]
        public void ShouldAddProductToUsersRatedProductsCollection()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Raters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, mockedProduct.Object, 5);

            // Assert
            Assert.AreEqual(1, mockedUser.Object.RatedProducts.Count);
            Assert.AreSame(mockedProduct.Object, mockedUser.Object.RatedProducts.First());
        }

        [Test]
        public void ShouldAddUserToProductRatersCollection()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>()));
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Raters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, mockedProduct.Object, 5);

            // Assert
            Assert.AreEqual(1, mockedProduct.Object.Raters.Count);
            Assert.AreSame(mockedUser.Object, mockedProduct.Object.Raters.First());
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataUsersRepository()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.Update(It.IsAny<User>())).Verifiable();
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()));

            var mockedUser = new Mock<User>();
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Raters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, mockedProduct.Object, 5);

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
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Raters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, mockedProduct.Object, 5);

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
            mockedUser.Setup(u => u.RatedProducts).Returns(new List<Product>());
            var mockedProduct = new Mock<Product>();
            mockedProduct.Setup(u => u.Raters).Returns(new List<User>());

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.RateProduct(mockedUser.Object, mockedProduct.Object, 5);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
