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
    public class GetUserByEmail
    {
        [Test]
        public void ShouldThrowArgumentException_WhenEmptyUserEmailIsPassed()
        {
            // Arrange
            var email = "";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                usersService.GetUserByEmail(email));
        }

        [Test]
        public void ShouldThrowArgumentExceptionWithCorrectMessage_WhenEmptyUserEmailIsPassed()
        {
            // Arrange
            var email = "";
            var expectedExMessage = "User email cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                usersService.GetUserByEmail(email));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserEmailIsPassed()
        {
            // Arrange
            string email = null;
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                usersService.GetUserByEmail(email));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserEmailIsPassed()
        {
            // Arrange
            string email = null;
            var expectedExMessage = "User email cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var usersService = new UsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                usersService.GetUserByEmail(email));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase("elon@tesla.com")]
        [TestCase("elon@spacex.com")]
        public void ShouldCallAllMethodOfDataUsersRepository(string email)
        {
            // Arrange
            var user = new User() { UserName = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(new List<User> { user }.AsQueryable())
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            usersService.GetUserByEmail(email);

            // Assert
            mockedData.Verify(d => d.UsersRepository.All(), Times.Once);
        }

        [TestCase("elon@tesla.com")]
        [TestCase("elon@spacex.com")]
        public void ShouldReturnCorrectUserBasedOnEmailPassedInFirstQuery(string email)
        {
            // Arrange
            var user = new User() { UserName = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(new List<User> { user }.AsQueryable())
                .Verifiable();

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetUserByEmail(email);

            // Assert
            Assert.AreSame(user, result);
        }
    }
}
