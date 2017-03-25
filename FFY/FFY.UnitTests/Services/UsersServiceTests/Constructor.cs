using FFY.Data.Contracts;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDataIsPassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UsersService(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDataIsPassed()
        {
            // Arrange
            var expectedExMessage = "Data cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UsersService(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidDependenciesArePassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new UsersService(mockedData.Object));
        }
    }
}
