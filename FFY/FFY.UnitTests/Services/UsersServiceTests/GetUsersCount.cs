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
    public class GetUsersCount
    {
        [Test]
        public void ShouldReturnAllUsersCount_WhenNoSearchWordIsProvided()
        {
            // Arrange
            var users = new List<User>()
            {
                new User() { FirstName = "Elon", LastName = "Musk" },
                new User() { FirstName = "Jeff", LastName = "Bezos" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(users.AsQueryable);

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetUsersCount(null);

            // Assert
            Assert.AreEqual(users.Count, result);
        }

        [TestCase("elon", 1)]
        [TestCase("e", 2)]
        [TestCase("x", 0)]
        public void ShouldReturnCorrectUsersCount_WhenSearchWordIsProvided(string searchWord, 
            int expectedCount)
        {
            // Arrange
            var users = new List<User>()
            {
                new User() { FirstName = "Elon", LastName = "Musk", UserName="elon" },
                new User() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(users.AsQueryable);

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.GetUsersCount(searchWord);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
