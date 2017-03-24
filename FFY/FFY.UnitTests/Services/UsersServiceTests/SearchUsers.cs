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
    public class SearchUsers
    {
        [Test]
        public void ShouldReturnAllUsers_WhenNoSearchWordIsProvided()
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
            var result = usersService.SearchUsers(null, "", 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(users, result);
        }

        [Test]
        public void ShouldReturnCorrectUser_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "elon";

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
            var result = usersService.SearchUsers(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(users[0], result.First());
        }

        [Test]
        public void ShouldReturnCorrectUsersCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "e";

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
            var result = usersService.SearchUsers(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(users[0], result.First());
            Assert.AreSame(users[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersSortedByEmail_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<User>()
            {
                new User() { FirstName = "Elon", LastName = "Musk", UserName="musk" },
                new User() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" },
                new User() { FirstName = "Charles", LastName = "Xavier", UserName="charles" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(users.AsQueryable);

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.SearchUsers(searchWord, "email", 1, 10);

            // Assert
            Assert.AreSame(users[2], result.First());
            Assert.AreSame(users[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersSortedByName_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<User>()
            {
                new User() { FirstName = "Elon", LastName = "Musk", UserName="elon" },
                new User() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" },
                new User() { FirstName = "Charles", LastName = "Xavier", UserName="charles" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.UsersRepository.All())
                .Returns(users.AsQueryable);

            var usersService = new UsersService(mockedData.Object);

            // Act
            var result = usersService.SearchUsers(searchWord, "name", 1, 10);

            // Assert
            Assert.AreSame(users[2], result.First());
            Assert.AreSame(users[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var searchWord = "e";

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
            var result = usersService.SearchUsers(searchWord, "", 2, 1);

            // Assert
            Assert.AreSame(users[1], result.First());
        }

        [Test]
        public void ShouldReturnNoUsers_WhenNoUsersAreMatchingTheCriterias()
        {
            // Arrange
            var searchWord = "qwerty";

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
            var result = usersService.SearchUsers(searchWord, "", 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
