﻿using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.ChatUsersServiceTests
{
    [TestFixture]
    public class AddChatUser
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullChatUserIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => chatUsersService.AddChatUser(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullChatUserIsPassed()
        {
            // Arrange
            var expectedExMessage = "Chat user cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                chatUsersService.AddChatUser(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataChatUsersRepository()
        {
            // Arrange
            var mockedChatUser = new Mock<ChatUser>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.Add(It.IsAny<ChatUser>()))
                .Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            chatUsersService.AddChatUser(mockedChatUser.Object);

            // Assert
            mockedData.Verify(d =>
                d.ChatUsersRepository.Add(mockedChatUser.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedChatUser = new Mock<ChatUser>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ChatUsersRepository.Add(It.IsAny<ChatUser>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var chatUsersService = new ChatUsersService(mockedData.Object);

            // Act
            chatUsersService.AddChatUser(mockedChatUser.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
