using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.SupportChatControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullChatUsersServiceIsPassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SupportChatController(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullChatUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Chat users service cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new SupportChatController(null));

            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenChatUsersServiceIsPassed()
        {
            // Arrange
            var mockedChatUsersService = new Mock<IChatUsersService>();

            // Act and Assert
            Assert.DoesNotThrow(() => 
                new SupportChatController(mockedChatUsersService.Object));
        }
    }
}
