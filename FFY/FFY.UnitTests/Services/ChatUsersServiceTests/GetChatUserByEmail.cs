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

namespace FFY.UnitTests.Services.ChatUsersServiceTests
{
    [TestFixture]
    public class GetChatUserByEmail
    {
        [Test]
        public void ShouldThrowArgumentException_WhenEmptyChatUserEmailIsPassed()
        {
            // Arrange
            var email = "";
            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                chatUsersService.GetChatUserByEmail(email));
        }

        [Test]
        public void ShouldThrowArgumentExceptionWithCorrectMessage_WhenEmptyChatUserEmailIsPassed()
        {
            // Arrange
            var email = "";
            var expectedExMessage = "Chat user email cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                chatUsersService.GetChatUserByEmail(email));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullChatUserEmailIsPassed()
        {
            // Arrange
            string email = null;
            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                chatUsersService.GetChatUserByEmail(email));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullChatUserEmailIsPassed()
        {
            // Arrange
            string email = null;
            var expectedExMessage = "Chat user email cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                chatUsersService.GetChatUserByEmail(email));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase("elon@tesla.com")]
        [TestCase("elon@solarcity.com")]
        public void ShouldCallAllMethodOfDataChatUsersRepository(string email)
        {
            // Arrange
            var chatUser = new ChatUser() { Email = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.All())
                .Returns(new List<ChatUser> { chatUser }.AsQueryable())
                .Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            chatUsersService.GetChatUserByEmail(email);

            // Assert
            mockedData.Verify(d => d.ChatUsersRepository.All(), Times.Once);
        }

        [TestCase("elon@hyperloop.com")]
        [TestCase("elon@spacex.com")]
        public void ShouldReturnCorrectChatUserBasedOnEmailPassedInFirstQuery(string email)
        {
            // Arrange
            var chatUser = new ChatUser() { Email = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.All())
                .Returns(new List<ChatUser> { chatUser }.AsQueryable())
                .Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            var result = chatUsersService.GetChatUserByEmail(email);

            // Assert
            Assert.AreSame(chatUser, result);
        }
    }
}
