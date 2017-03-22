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
    public class GetUserById
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
                usersService.GetUserById(id));
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
                usersService.GetUserById(id));
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
                usersService.GetUserById(id));
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
                usersService.GetUserById(id));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void ShouldCallGetByIdMethodOfDataUsersRepository(string id)
        {
            // Arrange
            var user = new User() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<int>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.GetUserById(id);

            // Assert
            mockedData.Verify(d => d.UsersRepository.GetById(id), Times.Once);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void ShouldReturnCorrectUser(string id)
        {
            // Arrange
            var user = new User() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.GetById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetUserById(id);

            // Assert
            Assert.AreEqual(user, result);
        }
    }
}
