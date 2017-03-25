using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.ChatUsersServiceTests
{
    [TestFixture]
    class GetChatUsers
    {
        [Test]
        public void ShouldCallAllMethodOfDataChatUsersRepository()
        {
            // Arrange
            var chatUsers = new List<ChatUser>() {
                new ChatUser() { Email = "elon@tesla.com" },
                new ChatUser() { Email = "elon@spacex.com" }
            }
            .AsQueryable();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.All())
                .Returns(chatUsers)
                .Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            chatUsersService.GetChatUsers();

            // Assert
            mockedData.Verify(d => d.ChatUsersRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnListWithAllChatUsers()
        {
            // Arrange
            var chatUsers = new List<ChatUser>() {
                new ChatUser() { Email = "elon@solarcity.com" },
                new ChatUser() { Email = "elon@hyperloop.com" }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.All())
                .Returns(chatUsers.AsQueryable())
                .Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            var result = chatUsersService.GetChatUsers();

            // Assert
            CollectionAssert.AreEqual(chatUsers, result);
        }
    }
}
