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
